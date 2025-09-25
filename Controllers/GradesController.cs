
using System.Security.Claims;
using lms_test1.Data;
using lms_test1.Models.DTO.Student;
using lms_test1.Models.DTO.Section;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lms_test1.Controllers;

[Authorize(Policy = "VerifiedOnly", Roles = "HeadTeacher,Teacher")]
public class GradesController : Controller
{
    private readonly ApplicationDbContext _context;

    public GradesController(ApplicationDbContext context)
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

    public async Task<IActionResult> SectionStudents(int id)
    {
        var section = await _context.Sections
            .FirstOrDefaultAsync(s => s.Id == id);
        
        if (section == null)
        {
            return NotFound();
        }

        // trim the student data to include only necessary information
        var students = await _context.Students
            .OrderBy(st => st.Gender)
            .ThenBy(st => st.LastName)
            .ThenBy(st => st.FirstName)
            .Where(st => st.SectionId == id)
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

        return View(dto);
    }

    public async Task<IActionResult> StudentGrades(int id)
    {        
        var student = await _context.Students
            .Include(st => st.Scores!)
                .ThenInclude(sc => sc.TeacherSubject)
                    .ThenInclude(ts => ts.Subject)
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

        var students = await _context.Students
            .Where(st => st.SectionId == sectionId &&
                        (st.LastName.ToLower().Contains(searchTerm.ToLower()) ||
                         st.FirstName.ToLower().Contains(searchTerm.ToLower())
                        ))
            .Select(st => new StudentInListDTO
            (
                st.Id,
                st.FirstName,
                st.LastName,
                st.Gender,
                st.MiddleName
            ))
            .ToListAsync();

        return PartialView("_StudentTableRows", students);
    }
}