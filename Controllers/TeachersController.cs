using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lms_test1.Areas.Identity.Data;
using lms_test1.Data;
using Microsoft.AspNetCore.Identity;
using lms_test1.Models.ViewModels.Teachers;
using Microsoft.AspNetCore.Authorization;
using lms_test1.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace lms_test1.Controllers;

[Authorize(Roles = "Admin")]
public class TeachersController : Controller
{
    private readonly UserManager<LMSUser> _userManager;
    private readonly ApplicationDbContext _context;

    public TeachersController(UserManager<LMSUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    // GET: Teachers
    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users
        .OrderBy(u => u.Verified)
        .ToListAsync();

        var userWithRoles = new List<UserWithRolesViewModel>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userWithRoles.Add(new UserWithRolesViewModel
            {
                Id = user.Id,
                Initials = $"{user.LastName}, {user.FirstName[0]}",
                Verified = user.Verified,
                Role = roles.FirstOrDefault() ?? ""
            });
        }

        return View(userWithRoles);
    }

    // GET: Teachers/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var userEntity = await _userManager.Users
            .Include(u => u.AdvisorySection)
            .Include(u => u.TeacherSubjects)
                .ThenInclude(ts => ts.Subject)
            .Include(u => u.TeacherSubjects)
                .ThenInclude(ts => ts.Sections)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (userEntity == null)
        {
            return NotFound();
        }

        var user = new LMSUserDetailViewModel
        {
            Id = userEntity.Id,
            Verified = userEntity.Verified,
            UserName = userEntity.UserName ?? string.Empty,
            Email = userEntity.Email ?? string.Empty,
            FirstName = userEntity.FirstName,
            LastName = userEntity.LastName,
            AdvisorySection = userEntity.AdvisorySection,
            TeacherSubjects = userEntity.TeacherSubjects
            .Select(ts => new TeacherSubjectDetail
            {
                SubjectName = ts.Subject.Name,
                SectionNames = ts.Sections.Select(s => s.Name).ToList()
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

        if (result.Succeeded)
            return RedirectToAction("Index");

        foreach (var error in result.Errors)
            ModelState.AddModelError("", error.Description);



        return View(model);

        // ViewData["AdvisorySectionId"] = new SelectList(_userManager.Sections, "Id", "Id", user.AdvisorySectionId);
        // return View(user);
    }

    // GET: Teachers/Assign/5
    public async Task<IActionResult> Assign(string id)
    {
        if (string.IsNullOrEmpty(id))
            return NotFound();

        var user = await _userManager.Users
            .Include(u => u.AdvisorySection)
            .Include(u => u.TeacherSubjects)
                .ThenInclude(ts => ts.Subject)
            .Include(u => u.TeacherSubjects)
                .ThenInclude(ts => ts.Sections)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return NotFound();

        var groupedSubjects = await _context.Subjects
            .OrderBy(s => s.Track)
            .ThenBy(s => s.Name)
            .GroupBy(s => s.Track)
            .ToListAsync();

        var groupedSections = await _context.Sections
            .Where(sec => sec.AdviserId == null || sec.AdviserId == user.Id)
            .OrderBy(sec => sec.YearLevel)
            .ThenBy(sec => sec.Name)
            .GroupBy(sec => sec.YearLevel)
            .ToListAsync();

        //get role of user
        var userRoles = await _userManager.GetRolesAsync(user);

        //get roles from database
        var roles = await _context.Roles.ToListAsync();

        // var allSections = await _context.Sections
        //     .OrderBy(sec => sec.YearLevel)
        //     .ThenBy(sec => sec.Name)
        //     .GroupBy(sec => sec.YearLevel)
        //     .Select(g => new {
        //         year = g.Key,
        //         sections = g.Select(s => new { id = s.Id, name = s.Name }).ToList()
        //     })
        //     .ToListAsync();

        var vm = new AssignTeacherViewModel
        {
            Role = userRoles.FirstOrDefault() ?? "",
            Roles = roles.Select(r => r.Name).ToList(),
            TeacherId = user.Id,
            TeacherName = $"{user.LastName}, {user.FirstName} {(string.IsNullOrEmpty(user.MiddleName) ? "" : user.MiddleName[0].ToString())}",
            AdvisorySectionId = user.AdvisorySection?.Id,
            SelectedSubjectIds = user.TeacherSubjects
                .Where(ts => ts.Subject != null)
                .Select(ts => ts.Subject!.Id)
                .ToList(),
            SubjectSections = user.TeacherSubjects.Select(ts => new SubjectSectionSelection
            {
                SubjectId = ts.SubjectId,
                SectionIds = ts.Sections.Select(s => s.Id).ToList()
            }).ToList()
        };

        var subjects = await _context.Subjects.ToListAsync();
        var sections = await _context.Sections.ToListAsync();

        var subjectSectionOptions = subjects.ToDictionary(
            subj => subj.Id,
            subj => sections
                .Where(sec => IsSectionAllowedForSubject(subj, sec))
                .OrderBy(sec => sec.YearLevel)
                .ThenBy(sec => sec.Name)
                .ToList()
        );

        ViewData["AllSections"] = subjectSectionOptions;
        ViewData["AllSectionsGrouped"] = groupedSections;
        ViewData["AllSubjectsGrouped"] = groupedSubjects;

        return View(vm);

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

    // POST: Teachers/Assign/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Assign(AssignTeacherViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // Optional: log validation errors for debugging
            foreach (var entry in ModelState)
            {
                foreach (var error in entry.Value.Errors)
                {
                    Console.WriteLine($"Property: {entry.Key}, Error: {error.ErrorMessage}");
                }
            }
            return View(model);
        }

        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == model.TeacherId);

        if (user == null)
            return NotFound();

        await using var transaction = await _context.Database.BeginTransactionAsync();

        // Update advisory section
        user.AdvisorySection = model.AdvisorySectionId.HasValue
            ? await _context.Sections.FindAsync(model.AdvisorySectionId.Value)
            : null;

        // Get all TeacherSubjects currently in DB for this teacher
        var existingTeacherSubjects = await _context.TeacherSubjects
            .Include(ts => ts.Sections)
            .Where(ts => ts.TeacherId == model.TeacherId)
            .ToListAsync();

        var selectedSubjectIds = model.SubjectSections.Select(ss => ss.SubjectId).ToHashSet();

        // Remove TeacherSubjects that are no longer selected
        var toRemove = existingTeacherSubjects
            .Where(ts => !selectedSubjectIds.Contains(ts.SubjectId))
            .ToList();

        if (toRemove.Any())
        {
            _context.TeacherSubjects.RemoveRange(toRemove);
        }

        // Add or update TeacherSubjects
        foreach (var subjectSelection in model.SubjectSections)
        {
            var existing = existingTeacherSubjects
                .FirstOrDefault(ts => ts.SubjectId == subjectSelection.SubjectId);

            if (existing != null)
            {
                // Compare current vs new section IDs
                var currentIds = existing.Sections.Select(s => s.Id).ToHashSet();
                var newIds = subjectSelection.SectionIds.ToHashSet();

                if (!currentIds.SetEquals(newIds))
                {
                    existing.Sections.Clear();

                    var sections = await _context.Sections
                        .Where(s => newIds.Contains(s.Id))
                        .ToListAsync();

                    foreach (var section in sections)
                    {
                        existing.Sections.Add(section);
                    }
                }
            }
            else
            {
                // Create new TeacherSubject
                var sections = await _context.Sections
                    .Where(s => subjectSelection.SectionIds.Contains(s.Id))
                    .ToListAsync();

                var teacherSubject = new TeacherSubject
                {
                    TeacherId = model.TeacherId,
                    SubjectId = subjectSelection.SubjectId,
                    Sections = sections
                };

                _context.TeacherSubjects.Add(teacherSubject);
            }
        }

        await _userManager.UpdateAsync(user);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return RedirectToAction("Index");
    }

    // GET: Teachers/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var lMSUser = await _userManager.Users
            .Include(u => u.AdvisorySection)
            .Include(u => u.TeacherSubjects)
                .ThenInclude(ts => ts.Subject)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (lMSUser == null)
        {
            return NotFound();
        }

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
        if (user == null)
            return NotFound();

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
        if (id == null)
        {
            return NotFound();
        }

        var userEntity = await _userManager.Users
            .Include(u => u.AdvisorySection)
            .Include(u => u.TeacherSubjects)
                .ThenInclude(ts => ts.Subject)
            .Include(u => u.TeacherSubjects)
                .ThenInclude(ts => ts.Sections)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (userEntity == null)
        {
            return NotFound();
        }

        var user = new LMSUserDetailViewModel
        {
            Id = userEntity.Id,
            Verified = userEntity.Verified,
            UserName = userEntity.UserName ?? string.Empty,
            Email = userEntity.Email ?? string.Empty,
            FirstName = userEntity.FirstName,
            LastName = userEntity.LastName,
            AdvisorySection = userEntity.AdvisorySection,
            TeacherSubjects = userEntity.TeacherSubjects
            .Select(ts => new TeacherSubjectDetail
            {
                SubjectName = ts.Subject.Name,
                SectionNames = ts.Sections.Select(s => s.Name).ToList()
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
        if (user == null)
            return NotFound();
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

