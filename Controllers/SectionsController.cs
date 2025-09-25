using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lms_test1.Data;
using lms_test1.Models;
using Microsoft.AspNetCore.Authorization;

namespace lms_test1.Controllers;

[Authorize(Policy = "VerifiedOnly", Roles = "Admin,HeadTeacher")]
public class SectionsController : Controller
{
    private readonly ApplicationDbContext _context;

    private readonly List<string> _strands = new List<string> { "STEM", "ABM", "HUMSS", "GAS", "TVL", "ICT", "Home Economics", "ICT", "Industrial Arts", "Agri-Fishery Arts", "Arts and Design", "Sports" };

    public SectionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Section
    public async Task<IActionResult> Index()
    {
        return View(
            await _context.Sections
                .OrderBy(s => s.YearLevel)
                .ToListAsync()
        );
    }

    // GET: Section/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var section = await _context.Sections
            .Include(s => s.Students)
            .Include(s => s.TeacherSubjects!)
                .ThenInclude(ts => ts.Subject)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (section == null)
        {
            return NotFound();
        }

        var sectionDetails = new Models.ViewModels.Sections.SectionDetails
        {
            Section = section,
            Students = section.Students!
                .Select(s => new Models.DTO.Student.StudentInListDTO(
                    s.Id,
                    s.LastName,
                    s.FirstName,
                    s.Gender,
                    s.MiddleName ?? ""
                ))
                .OrderBy(s => s.LastName)
                .ThenBy(s => s.FirstName)
                .ToList(),
            Subjects = section.TeacherSubjects!
                .Select(ts => ts.Subject)
                .ToList()
        };

        return View(sectionDetails);
    }

    // GET: Section/Create
    public IActionResult Create()
    {
        ViewData["Strands"] = _strands;
        return View();
    }

    // POST: Section/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Strand,YearLevel")] Section section)
    {
        if (ModelState.IsValid)
        {
            _context.Add(section);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(section);
    }

    // GET: Section/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var section = await _context.Sections.FindAsync(id);
        if (section == null)
        {
            return NotFound();
        }
        ViewData["Strands"] = _strands;
        return View(section);
    }

    // POST: Section/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Strand,YearLevel")] Section section)
    {
        if (id != section.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(section);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SectionExists(section.Id))
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
        return View(section);
    }

    // GET: Section/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var section = await _context.Sections
            .FirstOrDefaultAsync(m => m.Id == id);
        if (section == null)
        {
            return NotFound();
        }

        return View(section);
    }

    // POST: Section/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var section = await _context.Sections.FindAsync(id);
        if (section != null)
        {
            _context.Sections.Remove(section);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SectionExists(int id)
    {
        return _context.Sections.Any(e => e.Id == id);
    }

    // Search Sections (AJAX/partial)
    public IActionResult SearchSections(string term)
    {
        var sections = _context.Sections
            .Where(s => string.IsNullOrEmpty(term) || s.Name.ToLower().Contains(term.ToLower()))
            .OrderBy(s => s.YearLevel)
            .ToList();

        // You should create a partial view _SectionTableRows.cshtml for this to work
        return PartialView("_SectionTableRows", sections);
    }
}

