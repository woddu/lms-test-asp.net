using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lms_test1.Data;
using lms_test1.Models;
using Microsoft.AspNetCore.Authorization;



namespace lms_test1.Controllers;

[Authorize(Policy = "VerifiedOnly", Roles = "Admin, HeadTeacher")]
public class StudentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public StudentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Student
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Students
            .Include(s => s.Section);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Student/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students
            .Include(s => s.Section)
            .FirstOrDefaultAsync(m => m.Id == id);
            
        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    // GET: Student/Create
    public async Task<IActionResult> Create()
    {
        // grouped selection of sections by year level
        var sections = await _context.Sections
            .OrderBy(s => s.YearLevel)
            .ThenBy(s => s.Name)
            .GroupBy(s => s.YearLevel)
            .ToListAsync();
        ViewData["SectionId"] = sections;
        ViewData["Gender"] = new List<(string label, char value)>
            {
                ("Male", 'M'),
                ("Female", 'F'),
                ("Other", 'O')
            };
        return View();
    }

    // POST: Student/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,LastName,FirstName,MiddleName,Gender,Age,BirthDate,Address,SectionId")] Student student)
    {
        if (ModelState.IsValid)
        {
            _context.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        var sections = await _context.Sections
            .OrderBy(s => s.YearLevel)
            .ThenBy(s => s.Name)
            .GroupBy(s => s.YearLevel)
            .ToListAsync();
        ViewData["SectionId"] = sections;
        ViewData["Gender"] = new List<(string label, char value)>
            {
                ("Male", 'M'),
                ("Female", 'F'),
                ("Other", 'O')
            };
        return View(student);
    }

    // GET: Student/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        // grouped selection of sections by year level
        var sections = await _context.Sections
            .OrderBy(s => s.YearLevel)
            .ThenBy(s => s.Name)
            .GroupBy(s => s.YearLevel)
            .ToListAsync();
        ViewData["SectionId"] = sections;
        ViewData["Gender"] = new List<(string label, char value)>
            {
                ("Male", 'M'),
                ("Female", 'F'),
                ("Other", 'O')
            };
        return View(student);
    }

    // POST: Student/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,LastName,FirstName,MiddleName,Gender,Age,BirthDate,Address,SectionId")] Student student)
    {
        if (id != student.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(student.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        // grouped selection of sections by year level
        var sections = await _context.Sections
            .OrderBy(s => s.YearLevel)
            .ThenBy(s => s.Name)
            .GroupBy(s => s.YearLevel)
            .ToListAsync();
        ViewData["SectionId"] = sections;
        ViewData["Gender"] = new List<(string label, char value)>
            {
                ("Male", 'M'),
                ("Female", 'F'),
                ("Other", 'O')
            };
        return View(student);
    }

    // GET: Student/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students
            .Include(s => s.Section)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    // POST: Student/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null)
        {
            _context.Students.Remove(student);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool StudentExists(int id)
    {
        return _context.Students.Any(e => e.Id == id);
    }

    // AJAX: GET: Students/SearchStudents
    public async Task<IActionResult> SearchStudents(string term)
    {
        var studentsQuery = _context.Students.Include(s => s.Section).AsQueryable();
        if (!string.IsNullOrWhiteSpace(term))
        {
            var loweredTerm = term.ToLower();
            studentsQuery = studentsQuery.Where(s =>
                s.LastName.ToLower().Contains(loweredTerm) ||
                s.FirstName.ToLower().Contains(loweredTerm) ||
                s.MiddleName.ToLower().Contains(loweredTerm)
            );
        }
        var students = await studentsQuery.ToListAsync();
        return PartialView("_StudentTableRows", students);
    }
}

