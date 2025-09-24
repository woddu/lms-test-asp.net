using System.Security.Claims;
using lms_test1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lms_test1.Controllers;

[Authorize(Policy = "VerifiedOnly", Roles = "Teacher")]
public class AdvisoryController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdvisoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var advisories = await _context.Sections
            .Where(s => s.AdviserId == userId)
            .ToListAsync();

        return View(advisories);
    }

    public async Task<IActionResult> Students(int id)
    {
        var section = await _context.Sections
            .Include(
                s => s.Students!
                .OrderBy(st => st.Gender)
                .ThenBy(st => st.LastName)
                .ThenBy(st => st.FirstName)
            )
                .ThenInclude(st => st.Scores!)
                    .ThenInclude(sc => sc.TeacherSubject)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (section == null)
        {
            return NotFound();
        }

        return View(section);
    }
    
    public async Task<IActionResult> StudentDetails(int id)
    {
        var student = await _context.Students
            .Include(st => st.Scores!)
                .ThenInclude(sc => sc.TeacherSubject)
            .FirstOrDefaultAsync(st => st.Id == id);

        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

}