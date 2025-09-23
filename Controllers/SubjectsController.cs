using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lms_test1.Data;
using lms_test1.Models;
using Microsoft.AspNetCore.Authorization;

namespace lms_test1.Controllers
{
    [Authorize(Policy = "VerifiedOnly", Roles = "Admin, HeadTeacher")]
    public class SubjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly List<string> _tracks = new List<string> { "Core Subject (All Tracks)", "Academic Track (except Immersion)", "TVL/ Sports/ Arts and Design Track", "Work Immersion/ Culminating Activity (for Academic Track)" };

        public SubjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Subject
        public async Task<IActionResult> Index()
        {
            return View(
                await _context.Subjects
                    .ToListAsync()
                );
        }

        // GET: Subject/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects
                .Include(s => s.TeacherSubjects)
                    .ThenInclude(ts => ts.Teacher)
                .Include(s => s.TeacherSubjects)
                    .ThenInclude(ts => ts.Sections)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (subject == null)
            {
                return NotFound();
            }

            var subjectDetails = new Models.ViewModels.Subject.SubjectDetails
            {
                Subject = subject,
                Teachers = subject.TeacherSubjects
                    .Select(ts => new Models.DTO.Teacher.TeacherInListDTO(
                        ts.Teacher.Id,
                        ts.Teacher.LastName,
                        ts.Teacher.FirstName,
                        ts.Teacher.MiddleName ?? ""
                    ))
                    .ToList(),
                Sections = subject.TeacherSubjects
                    .SelectMany(ts => ts.Sections)
                    .ToList()
            };

            return View(subjectDetails);
        }

        // GET: Subject/Create
        public IActionResult Create()
        {
            ViewData["Tracks"] = _tracks;
            return View();
        }

        // POST: Subject/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Track")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        // GET: Subject/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            ViewData["Tracks"] = _tracks;
            return View(subject);
        }

        // POST: Subject/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Track")] Subject subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.Id))
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
            return View(subject);
        }

        // GET: Subject/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Subject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(int id)
        {
            return _context.Subjects.Any(e => e.Id == id);
        }

        public IActionResult SearchSubjects(string term)
        {
            var subjects = _context.Subjects
                .Where(s => string.IsNullOrEmpty(term) || s.Name.ToLower().Contains(term.ToLower()))
                .ToList();

            return PartialView("_SubjectTableRows", subjects);
        }
    }
}
