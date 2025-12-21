using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using lms_test1.Models;
using Microsoft.AspNetCore.Authorization;
using lms_test1.Data;
using Microsoft.EntityFrameworkCore;

namespace lms_test1.Controllers;

[Authorize(Policy = "VerifiedOnly")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {        
        var announcements = await _context.Announcements
            .OrderByDescending(a => a.DatePosted)
            .ToListAsync();

        return View(announcements);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
