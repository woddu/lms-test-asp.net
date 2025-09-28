
using System.Security.Claims;
using lms_test1.Data;
using lms_test1.Models.ViewModels.Grade;
using lms_test1.Models;
using lms_test1.Models.DTO.Student;
using lms_test1.Models.DTO.TeacherSubject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lms_test1.Models.DTO.Score;

namespace lms_test1.Controllers;

[Authorize(Policy = "VerifiedOnly", Roles = "HeadTeacher,Teacher")]
public class GradesController : Controller
{
    private readonly ApplicationDbContext _context;

    private readonly IAuthorizationService _authorizationService;

    public GradesController(ApplicationDbContext context, IAuthorizationService authorizationService)
    {
        _context = context;
        _authorizationService = authorizationService;
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveSubjectScoresChanges([FromBody] TeacherSubjectDTO model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
            .Where(ms => ms.Value!.Errors.Count > 0)
            .Select(ms => new
            {
                Field = ms.Key,
                Errors = ms.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
            });
            return BadRequest(errors);
        }

        var ts = await _context.TeacherSubjects
            .Where(ts => ts.Id == model.Id)
            .FirstOrDefaultAsync();

        if (ts == null) return NotFound();

        var result = await _authorizationService.AuthorizeAsync(User, ts, "SameOwnerOfTeacherSubject");
        if (!result.Succeeded) return Forbid();

        ApplyChanges(model, ts, _context);

        await _context.SaveChangesAsync();

        var updatedTs = await _context.TeacherSubjects
            .Where(t => t.Id == ts.Id)
            .FirstOrDefaultAsync();

        if (updatedTs == null) return NotFound();

        return Json(new { 
            success = true, 
            redirectUrl = Url.Action("SubjectDetails", "Grades", new { teacherSubjectId = updatedTs.Id }) 
        });

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
                    .ThenInclude(
                        s => s.Students!
                            .OrderBy(st => st.Gender)
                            .ThenBy(st => st.LastName)
                            .ThenBy(st => st.FirstName)
                    )
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

    //Get the scores of a specific student for a specific teacher's subject
    public async Task<IActionResult> StudentScores(int studentId, int teacherSubjectId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var student = await _context.Students
            .Include(st => st.Scores!.Where(sc => sc.TeacherSubject.Id == teacherSubjectId))
                .ThenInclude(sc => sc.TeacherSubject)
                    .ThenInclude(ts => ts.Subject)
            .Include(st => st.Scores!.Where(sc => sc.TeacherSubject.Id == teacherSubjectId))
                .ThenInclude(sc => sc.TeacherSubject)
                    .ThenInclude(ts => ts.TeacherSubjectSections)
                        .ThenInclude(tss => tss.Section)
            .FirstOrDefaultAsync(st => st.Id == studentId);

        if (student == null) return NotFound();

        if (student.Scores == null || student.Scores.Count == 0)
        {
            // create a new score entry for the student for this teacher subject
            var teacherSubject = await _context.TeacherSubjects
                .Include(ts => ts.TeacherSubjectSections)
                    .ThenInclude(tss => tss.Section)
                .FirstOrDefaultAsync(ts => ts.Id == teacherSubjectId && ts.TeacherId == userId);
            if (teacherSubject == null) return NotFound();
            var newScore = new Score
            {
                StudentId = student.Id,
                Student = student,
                TeacherSubject = teacherSubject,
                TeacherSubjectId = teacherSubject.Id,
            };
            _context.Scores.Add(newScore);
            await _context.SaveChangesAsync();

            var viewModel = new StudentScoresViewModel
            {
                TeacherSubject = teacherSubject,
                Student = new StudentInListDTO
            (
                student.Id,
                student.FirstName,
                student.LastName,
                student.Gender,
                student.MiddleName
            ),
                StudentScore = newScore
            };
            return View(viewModel);

        }
        else
        {
            // ensure the teacher subject belongs to the logged-in teacher
            var score = student.Scores.First();
            if (score.TeacherSubject.TeacherId != userId) return NotFound();
            var viewModel = new StudentScoresViewModel
            {
                TeacherSubject = student.Scores!.First().TeacherSubject,
                Student = new StudentInListDTO
            (
                student.Id,
                student.FirstName,
                student.LastName,
                student.Gender,
                student.MiddleName
            ),
                StudentScore = student.Scores!.First()
            };
            return View(viewModel);
        }

    }

    public async Task<IActionResult> SaveStudentScoresChanges([FromBody] ScoreDTO model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
            .Where(ms => ms.Value!.Errors.Count > 0)
            .Select(ms => new
            {
                Field = ms.Key,
                Errors = ms.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
            });
            return BadRequest(errors);
        }

        var score = await _context.Scores
            .Include(s => s.TeacherSubject)
            .Where(s => s.Id == model.Id)
            .FirstOrDefaultAsync();

        if (score == null) return NotFound();
        
        var result = await _authorizationService.AuthorizeAsync(User, score.TeacherSubject, "SameOwnerOfTeacherSubject");
        if (!result.Succeeded) return Forbid();

        ApplyChanges(model, score, _context);

        await _context.SaveChangesAsync();

        var updatedScore = await _context.Scores
            .Include(s => s.TeacherSubject)
            .Include(s => s.Student)
            .Where(s => s.Id == score.Id)
            .FirstOrDefaultAsync();

        if (updatedScore == null) return NotFound();

        return Json(new
        {
            success = true,
            redirectUrl = Url.Action("StudentScores", "Grades", new { studentId = updatedScore.StudentId, teacherSubjectId = updatedScore.TeacherSubjectId })
        });

    }

    public async Task<IActionResult> SearchStudent(string searchTerm, int sectionId, int teacherSubjectId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var teacherSubject = await _context.TeacherSubjects
            .Include(ts => ts.TeacherSubjectSections.Where(s => s.Section.Id == sectionId))
                .ThenInclude(tss => tss.Section)
                    .ThenInclude
                    (
                        s => s.Students!
                            .Where(st =>
                                string.IsNullOrEmpty(searchTerm) ||
                                st.LastName.ToLower().Contains(searchTerm.ToLower()) ||
                                st.FirstName.ToLower().Contains(searchTerm.ToLower())
                            )
                            .OrderBy(st => st.Gender)
                            .ThenBy(st => st.LastName)
                            .ThenBy(st => st.FirstName)
                )
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

        return PartialView("_StudentTableRows", dto);

    }
    
    public static void ApplyChanges<TSource, TDest>(TSource source, TDest dest, DbContext ctx)
    {
        var srcProps = typeof(TSource).GetProperties();
        var destProps = typeof(TDest).GetProperties().ToDictionary(p => p.Name);

        var entry = ctx.Entry(dest!);

        foreach (var sp in srcProps)
        {
            if (!destProps.TryGetValue(sp.Name, out var dp)) continue;

            var newValue = sp.GetValue(source);
            var oldValue = dp.GetValue(dest);

            if (!Equals(newValue, oldValue))
            {
                dp.SetValue(dest, newValue);
                entry.Property(dp.Name).IsModified = true;
            }
        }
    }

}