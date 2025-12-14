using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lms_test1.Areas.Identity.Data;
using lms_test1.Data;
using Microsoft.AspNetCore.Identity;
using lms_test1.Models.ViewModels.TeachersManagement;
using Microsoft.AspNetCore.Authorization;
using lms_test1.Models;
using Microsoft.Data.Sqlite;
using lms_test1.Models.DTO.Section;

namespace lms_test1.Controllers;

[Authorize(Roles = "Admin")]
public class TeachersManagementController : Controller
{
    private readonly UserManager<LMSUser> _userManager;
    private readonly ApplicationDbContext _context;

    public TeachersManagementController(UserManager<LMSUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    // GET: Teachers
    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users
        .OrderBy(u => u.Verified)
            .ThenBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
        .ToListAsync();

        var userWithRoles = new List<UserWithRolesViewModel>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userWithRoles.Add(new UserWithRolesViewModel
            {
                Id = user.Id,
                Initials = $"{user.LastName}, {user.FirstName} {(string.IsNullOrEmpty(user.MiddleName) ? "" : user.MiddleName[0].ToString())}",
                Verified = user.Verified,
                Role = roles.FirstOrDefault() ?? ""
            });
        }

        return View(userWithRoles);
    }

    // GET: Teachers/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (id == null) return NotFound();        

        var userEntity = await _userManager.Users
            .Include(u => u.AdvisorySections)
            .Include(u => u.TeacherSubjects)
                .ThenInclude(ts => ts.Subject)
            .Include(u => u.TeacherSubjects)
                .ThenInclude(ts => ts.TeacherSubjectSections)
                    .ThenInclude(tss => tss.Section)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (userEntity == null) return NotFound();

        var user = new LMSUserDetailViewModel
        {
            Id = userEntity.Id,
            Verified = userEntity.Verified,
            UserName = userEntity.UserName ?? string.Empty,
            Email = userEntity.Email ?? string.Empty,
            FirstName = userEntity.FirstName,
            LastName = userEntity.LastName,
            AdvisorySections = userEntity.AdvisorySections!.ToList(),
            TeacherSubjects = userEntity.TeacherSubjects
            .Select(ts => new TeacherSubjectDetail
            {
                SubjectName = ts.Subject.Name,
                SectionNames = ts.TeacherSubjectSections.Select(tss => tss.Section.Name).ToList()
            })
            .ToList()
        };

        return View(user);
    }

    // GET: Teachers/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Teachers/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LMSUserCreateViewModel model)
    {
        var user = new LMSUser
        {
            UserName = model.UserName,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Verified = model.Verified,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        
        var roleResult = await _userManager.AddToRoleAsync(user, "Teacher");

        if (result.Succeeded && roleResult.Succeeded)
            return RedirectToAction("Index");

        foreach (var error in result.Errors)
            ModelState.AddModelError("", error.Description);



        return View(model);

        // ViewData["AdvisorySectionId"] = new SelectList(_userManager.Sections, "Id", "Id", user.AdvisorySectionId);
        // return View(user);
    }

    private bool IsSectionAllowedForSubject(Subject subject, Section section)
    {
        return subject.Track switch
        {
            "Core Subject (All Tracks)" => true, // all strands allowed

            "Academic Track (except Immersion)" =>
                section.Strand is "STEM" or "ABM" or "HUMSS" or "GAS",

            "TVL/ Sports/ Arts and Design Track" =>
                section.Strand is "TVL" or "Sports" or "Arts and Design",

            "Work Immersion/ Culminating Activity (for Academic Track)" =>
                section.Strand is "STEM" or "ABM" or "HUMSS" or "GAS",

            _ => false
        };
    }

    public async Task<IActionResult> Assign(string id)
    {
        if (string.IsNullOrEmpty(id))
            return NotFound();

        // Load user with related data
        var user = await _userManager.Users
            .Include(u => u.AdvisorySections)
            .Include(u => u.TeacherSubjects)
                .ThenInclude(ts => ts.Subject)
            .Include(u => u.TeacherSubjects)
                .ThenInclude(ts => ts.TeacherSubjectSections)
                    .ThenInclude(tss => tss.Section)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null) return NotFound();

        // Roles
        var userRoles = await _userManager.GetRolesAsync(user);
        var roles = await _context.Roles.Select(r => r.Name).ToListAsync();

        // Subjects (ordered once, reused)
        var subjects = await _context.Subjects
            .OrderBy(s => s.Track)
            .ThenBy(s => s.Name)
            .ToListAsync();

        var groupedSubjects = subjects.GroupBy(s => s.Track).ToList();

        // Sections (filtered + eager loaded once, reused)
        var sections = await _context.Sections
            .Where(sec => sec.AdviserId == null || sec.AdviserId == user.Id)
            .OrderBy(sec => sec.YearLevel)
            .ThenBy(sec => sec.Name)
            .Include(s => s.TeacherSubjectSections)
                .ThenInclude(tss => tss.TeacherSubject)
                    .ThenInclude(ts => ts.Subject)
            .ToListAsync();

        var groupedSections = sections.GroupBy(sec => sec.YearLevel).ToList();

        // Build subject-section options dictionary
        var subjectSectionOptions = subjects.ToDictionary(
            subj => subj.Id,
            subj => sections
                .Where(sec =>
                    IsSectionAllowedForSubject(subj, sec) &&
                    !sec.TeacherSubjectSections.Any(tss =>
                        tss.TeacherSubject.SubjectId == subj.Id &&
                        tss.TeacherSubject.TeacherId != user.Id
                    )
                )
                .Select(sec => new SectionDTO
                {
                    Id = sec.Id,
                    Name = sec.Name
                })
                .ToList()
        );

        // Construct ViewModel
        var vm = new AssignTeacherViewModelA
        {
            TeacherId = user.Id,
            TeacherName = $"{user.LastName}, {user.FirstName} {(string.IsNullOrEmpty(user.MiddleName) ? "" : user.MiddleName[0].ToString())}",
            Role = userRoles.FirstOrDefault() ?? "",
            Roles = roles,
            AdvisorySections = user.AdvisorySections!.ToList(),
            SubjectWithSections = user.TeacherSubjects.Select(ts => new SubjectWithSections
            {
                Subject = ts.Subject,
                Sections = ts.TeacherSubjectSections.Select(tss => tss.Section).ToList()
            }).ToList(),
            GroupedSections = groupedSections,
            GroupedSubjects = groupedSubjects,
            SubjectSectionOptions = subjectSectionOptions
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeTeacherRole(string teacherId, string newRole)
    {
        var teacher = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == teacherId);
        if (teacher == null) return NotFound();

        var currentRoles = await _userManager.GetRolesAsync(teacher);
        if (currentRoles.Any())
        {
            var removeResult = await _userManager.RemoveFromRolesAsync(teacher, currentRoles);
            if (!removeResult.Succeeded)
            {
                foreach (var error in removeResult.Errors)
                    ModelState.AddModelError("", error.Description);
                return RedirectToAction("Assign", new { id = teacherId });
            }
        }

        var addResult = await _userManager.AddToRoleAsync(teacher, newRole);
        if (!addResult.Succeeded)
        {
            foreach (var error in addResult.Errors)
                ModelState.AddModelError("", error.Description);
            return RedirectToAction("Assign", new { id = teacherId });
        }

        return RedirectToAction("Assign", new { id = teacherId });
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddAdvisorySection(string teacherId, int sectionId)
    {        
        var teacher = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == teacherId);
        if (teacher == null) return NotFound($"Teacher with ID {teacherId} was not found.");

        
        var section = await _context.Sections
            .FirstOrDefaultAsync(s => s.Id == sectionId);
        if (section == null) return NotFound($"Section with ID {sectionId} was not found.");

        Console.WriteLine($"Assigning advisory section {section.Name} to teacher {teacher.LastName}, {teacher.FirstName}");

        if (section.AdviserId != null && section.AdviserId != teacher.Id)
        {
            Console.WriteLine($"Section {section.Name} already has an adviser with ID {section.AdviserId}");
            ModelState.AddModelError("", "Section already has an adviser.");
            return RedirectToAction("Assign", new { id = teacherId });
        }
        
        section.AdviserId = teacher.Id;
        await _context.SaveChangesAsync();

        return RedirectToAction("Assign", new { id = teacherId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveAdvisorySection(string teacherId, int sectionId)
    {
        var teacher = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == teacherId);
        if (teacher == null) return NotFound($"Teacher with ID {teacherId} was not found.");

        var section = await _context.Sections
            .FirstOrDefaultAsync(s => s.Id == sectionId);
        if (section == null) return NotFound($"Section with ID {sectionId} was not found.");

        if (section.AdviserId != teacher.Id)
        {
            ModelState.AddModelError("", "Section is not assigned to this teacher.");
            return RedirectToAction("Assign", new { id = teacherId });
        }

        section.AdviserId = null;
        await _context.SaveChangesAsync();

        return RedirectToAction("Assign", new { id = teacherId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddTeacherSubject(string teacherId, int subjectId)
    {
        var teacher = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == teacherId);
        if (teacher == null) return NotFound($"Teacher with ID {teacherId} was not found.");

        var subject = await _context.Subjects
            .FirstOrDefaultAsync(s => s.Id == subjectId);
        if (subject == null) return NotFound($"Subject with ID {subjectId} was not found.");

        try
        {
            var teacherSubject = new TeacherSubject
            {
                TeacherId = teacher.Id,
                SubjectId = subject.Id
            };
            _context.TeacherSubjects.Add(teacherSubject);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqliteException sqliteEx &&
                                        sqliteEx.SqliteErrorCode == 19)
        {
            // Handle unique constraint violation
            ModelState.AddModelError("", "This subject is already assigned to the teacher.");
            return RedirectToAction("Assign", new { id = teacherId });
        }
        
        return RedirectToAction("Assign", new { id = teacherId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveTeacherSubject(string teacherId, int subjectId)
    {
        var teacher = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == teacherId);
        if (teacher == null) return NotFound($"Teacher with ID {teacherId} was not found.");
                
        var subject = await _context.Subjects
            .FirstOrDefaultAsync(s => s.Id == subjectId);
        if (subject == null) return NotFound($"Subject with ID {subjectId} was not found.");

        var teacherSubject = await _context.TeacherSubjects
            .Include(ts => ts.TeacherSubjectSections)
            .FirstOrDefaultAsync(ts => ts.TeacherId == teacher.Id && ts.SubjectId == subject.Id);
        if (teacherSubject == null) return NotFound();
        
        _context.TeacherSubjects.Remove(teacherSubject);
        await _context.SaveChangesAsync();

        return RedirectToAction("Assign", new { id = teacherId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddTeacherSubjectSection(string teacherId, int sectionId, int subjectId)
    {
        var teacher = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == teacherId);
        if (teacher == null) return NotFound($"Teacher with ID {teacherId} was not found.");

        
        return RedirectToAction("Assign", new { id = teacherId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveTeacherSubjectSection(string teacherId, int sectionId, int subjectId)
    {
        var teacher = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == teacherId);
        if (teacher == null) return NotFound($"Teacher with ID {teacherId} was not found.");

        
        return RedirectToAction("Assign", new { id = teacherId });
    }



    // GET: Teachers/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var lMSUser = await _userManager.Users
            .Include(u => u.AdvisorySections)
            .Include(u => u.TeacherSubjects)
                .ThenInclude(ts => ts.Subject)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (lMSUser == null) return NotFound();
        

        var viewModel = new LMSUserEditViewModel
        {
            Id = lMSUser.Id,
            Verified = lMSUser.Verified,
            FirstName = lMSUser.FirstName,
            MiddleName = lMSUser.MiddleName,
            LastName = lMSUser.LastName,
            Email = lMSUser.Email ?? string.Empty,
            UserName = lMSUser.UserName ?? string.Empty,
            AdvisorySectionOptions = await _context.Sections
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                })
                .ToListAsync(),
            Subjects = lMSUser.TeacherSubjects?
                .Select(s => new TeacherSubjectViewModel
                {
                    Id = s.Subject.Id,
                    Name = s.Subject.Name
                })
                .ToList()
        };

        return View(viewModel);
    }


    // POST: Teachers/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(LMSUserEditViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.Id);
        if (user == null) return NotFound();

        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.MiddleName = model.MiddleName;
        user.Verified = model.Verified;
        // user.AdvisorySection = await _context.Sections.FindAsync(model.AdvisorySectionId);
        user.Email = model.Email;
        user.UserName = model.UserName;

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
            return RedirectToAction("Index");

        foreach (var error in result.Errors)
            ModelState.AddModelError("", error.Description);
        // ViewData["AdvisorySectionId"] = new SelectList(_userManager.Sections, "Id", "Id", lMSUser.AdvisorySectionId);
        return View(model);
    }

    // GET: Teachers/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null) return NotFound();
        
        var userEntity = await _userManager.Users
            .Include(u => u.AdvisorySections)
            .Include(u => u.TeacherSubjects)
                .ThenInclude(ts => ts.Subject)
            .Include(u => u.TeacherSubjects)
                .ThenInclude(ts => ts.TeacherSubjectSections)
                    .ThenInclude(tss => tss.Section)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (userEntity == null) return NotFound();
        

        var user = new LMSUserDetailViewModel
        {
            Id = userEntity.Id,
            Verified = userEntity.Verified,
            UserName = userEntity.UserName ?? string.Empty,
            Email = userEntity.Email ?? string.Empty,
            FirstName = userEntity.FirstName,
            LastName = userEntity.LastName,
            AdvisorySections = userEntity.AdvisorySections!.ToList(),
            TeacherSubjects = userEntity.TeacherSubjects
            .Select(ts => new TeacherSubjectDetail
            {
                SubjectName = ts.Subject.Name,
                SectionNames = ts.TeacherSubjectSections.Select(tss => tss.Section.Name).ToList()
            })
            .ToList()
        };

        return View(user);
    }

    // POST: Teachers/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        var result = await _userManager.DeleteAsync(user);

        if (result.Succeeded)
            return RedirectToAction(nameof(Index));

        foreach (var error in result.Errors)
            ModelState.AddModelError("", error.Description);
        return View("Delete", user); // Optional: return to delete confirmation view
    }

    private bool LMSUserExists(string id)
    {
        return _userManager.Users.Any(e => e.Id == id);
    }
}

