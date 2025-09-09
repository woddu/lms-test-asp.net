using Microsoft.EntityFrameworkCore;
using lms_test1.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace lms_test1.Controllers;

[Authorize(Roles = "Admin")]
public class TeacherManagementController(UserManager<LMSUser> userManager) : Controller
{
    private readonly UserManager<LMSUser> _userManager = userManager;

    public async Task<IActionResult> Index()
    {
        // order by not verified
        var users = await _userManager.Users
            .OrderBy(u => u.Verified)
            .ToListAsync();
        return View(users); 
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        string email,
        string userName,
        string firstName,
        string lastName,
        string password
        )
    {
        var user = new LMSUser
        {
            UserName = userName,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            EmailConfirmed = true
        };
        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User");
            return RedirectToAction("Index");
        }
        return View("Error", result.Errors);
    }

}