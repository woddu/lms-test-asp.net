
using System.Security.Claims;
using lms_test1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lms_test1.Controllers;

[Authorize(Policy = "VerifiedOnly", Roles = "HeadTeacher,Teacher")]
public class GradeController : Controller
{
    private readonly ApplicationDbContext _context;

    public GradeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var teacherSubjects = await _context.TeacherSubjects
            .Include(ts => ts.Subject)
            .Include(ts => ts.Sections)
            .Where(ts => ts.TeacherId == userId)
            .ToListAsync();

        return View(teacherSubjects);
    }

    public async Task<IActionResult> Subject(int id)
    {
        var teacherSubject = await _context.TeacherSubjects
            .Include(ts => ts.Subject)
            .Include(ts => ts.Sections)
                .ThenInclude(s => s.Students!)
                    .ThenInclude(st => st.Scores)
            .FirstOrDefaultAsync(ts => ts.Id == id);

        if (teacherSubject == null)
        {
            return NotFound();
        }

        return View(teacherSubject);
    }
}