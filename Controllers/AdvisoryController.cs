using System.Security.Claims;
using lms_test1.Data;
using lms_test1.Models.DTO.Section;
using lms_test1.Models.DTO.Student;
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

    public async Task<IActionResult> Students(int sectionId)
    {
        var section = await _context.Sections
            .FirstOrDefaultAsync(s => s.Id == sectionId);

        if (section == null)
        {
            return NotFound();
        }

        var students = await _context.Students
            .Include(st => st.Scores!)
                .ThenInclude(sc => sc.TeacherSubject)
            .Where(st => st.SectionId == sectionId)
            .Select(st => new StudentInListDTO(
                st.Id,
                st.FirstName,
                st.LastName,
                st.Gender,
                st.MiddleName ?? ""
            ))
            .ToListAsync();

        var dto = new SectionWithStudentsDTO
        (
            Section: section,
            Students: students
        );

        return View(dto);
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

    public async Task<IActionResult> SearchStudent(string searchTerm, int sectionId)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return Json(new { results = new List<object>() });
        }
        
        var section = await _context.Sections
                    .FirstOrDefaultAsync(s => s.Id == sectionId);

        if (section == null)
        {
            return NotFound();
        }

        var students = await _context.Students
            .Where
            (
                st => st.SectionId == sectionId &&
                (
                    st.LastName.ToLower().Contains(searchTerm.ToLower()) ||
                    st.FirstName.ToLower().Contains(searchTerm.ToLower())
                )
            )
            .Select(st => new StudentInListDTO
            (
                st.Id,
                st.FirstName,
                st.LastName,
                st.Gender,
                st.MiddleName
            ))
            .ToListAsync();
        var dto = new SectionWithStudentsDTO
        (
            Section: section,
            Students: students
        );
        return PartialView("_StudentTableRows", dto);
    }

}