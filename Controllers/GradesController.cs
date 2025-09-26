
using System.Security.Claims;
using lms_test1.Data;
using lms_test1.Models.DTO.Student;
using lms_test1.Models.DTO.TeacherSubject;
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

    //Get all the teacher's subjects
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var teacherSubjects = await _context.TeacherSubjects
            .Include(ts => ts.Subject)
            .Where(ts => ts.TeacherId == userId)
            .ToListAsync();

        return View(teacherSubjects);
    }

    public async Task<IActionResult> SubjectDetails(int teacherSubjectId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var teacherSubject = await _context.TeacherSubjects
            .Include(ts => ts.Subject)
            .Include(ts => ts.TeacherSubjectSections)
                .ThenInclude(tss => tss.Section)
            .FirstOrDefaultAsync(ts => ts.Id == teacherSubjectId && ts.TeacherId == userId);

        if (teacherSubject == null) return NotFound();        

        return View(teacherSubject);
    }

    //Get the students in a section for a specific teacher's subject
    public async Task<IActionResult> SectionStudents(int sectionId, int teacherSubjectId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var teacherSubject = await _context.TeacherSubjects
            .Include(ts => ts.Subject)
            .Include(
                ts => ts.TeacherSubjectSections.Where(s => s.Section.Id == sectionId)
            )
                .ThenInclude(tss => tss.Section)
                    .ThenInclude(s => s.Students)
            .FirstOrDefaultAsync(ts => ts.Id == teacherSubjectId && ts.TeacherId == userId);

        if (teacherSubject == null) return NotFound();

        var dto = new TeacherSubjectWithStudentsDTO
        (
            TeacherSubject: teacherSubject,
            Students: teacherSubject.TeacherSubjectSections.First().Section.Students!
                .Select(st => new StudentInListDTO
                (
                    st.Id,
                    st.FirstName,
                    st.LastName,
                    st.Gender,
                    st.MiddleName
                ))
                .ToList()
        );

        return View(dto);
    }

    //Get the grades of a specific student for a specific teacher's subject
    public async Task<IActionResult> StudentGrades(int studentId, int teacherSubjectId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var student = await _context.Students
            .Include(st => st.Scores!.Where(sc => sc.TeacherSubject.Id == teacherSubjectId))
                .ThenInclude(sc => sc.TeacherSubject)
                    .ThenInclude(ts => ts.Subject)
            .FirstOrDefaultAsync(st => st.Id == studentId);

        if (student == null) return NotFound();
        

        return View(student);
    }

    public async Task<IActionResult> SearchStudent(string searchTerm, int sectionId, int teacherSubjectId)
    {        
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var teacherSubject = await _context.TeacherSubjects
            .Include(ts => ts.TeacherSubjectSections.Where(s => s.Section.Id == sectionId))
                .ThenInclude(tss => tss.Section)
                    .ThenInclude(s => s.Students!.Where(st => 
                        string.IsNullOrEmpty(searchTerm) ||
                        st.LastName.ToLower().Contains(searchTerm.ToLower()) ||
                        st.FirstName.ToLower().Contains(searchTerm.ToLower())
                ))
            .FirstOrDefaultAsync(ts => ts.Id == teacherSubjectId && ts.TeacherId == userId);

        if (teacherSubject == null) return NotFound();
        

        var students = teacherSubject!.TeacherSubjectSections.First().Section.Students!
            .Select(st => new StudentInListDTO
            (
                st.Id,
                st.FirstName,
                st.LastName,
                st.Gender,
                st.MiddleName
            ))
            .ToList();

        return PartialView("_StudentTableRows", students);
    }
}