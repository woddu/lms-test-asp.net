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
    
    public async Task<IActionResult> Section(int id)
    {
        var section = await _context.Sections
            .Include(s => s.Students)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (section == null)
        {
            return NotFound();
        }

        return View(section);
    }

}