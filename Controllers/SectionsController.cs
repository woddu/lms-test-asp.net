using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lms_test1.Data;
using lms_test1.Models;
using Microsoft.AspNetCore.Authorization;

namespace lms_test1.Controllers;

[Authorize(Roles = "Admin,HeadTeacher")]
public class SectionsController : Controller
{
    private readonly ApplicationDbContext _context;

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
            .FirstOrDefaultAsync(m => m.Id == id);
        if (section == null)
        {
            return NotFound();
        }

        return View(section);
    }

    // GET: Section/Create
    public IActionResult Create()
    {
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

