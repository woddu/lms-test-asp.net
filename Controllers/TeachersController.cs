using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lms_test1.Areas.Identity.Data;
using lms_test1.Data;
using Microsoft.AspNetCore.Identity;
using lms_test1.Models.ViewModels.Teachers;
using Microsoft.AspNetCore.Authorization;
using lms_test1.Models;

namespace lms_test1.Controllers;

[Authorize(Roles = "Admin")]
public class TeachersController(UserManager<LMSUser> userManager) : Controller
{
    private readonly UserManager<LMSUser> _userManager = userManager;

    // GET: Teachers
    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users
        .OrderBy(u => u.Verified)
        .ToListAsync();

    var userWithRoles = new List<UserWithRolesViewModel>();

    foreach (var user in users)
    {
        var roles = await _userManager.GetRolesAsync(user);
        userWithRoles.Add(new UserWithRolesViewModel
        {
            Id = user.Id,
            Initials = $"{user.LastName}, {user.FirstName[0]}",
            Verified = user.Verified,
            Role = roles.FirstOrDefault() ?? ""
        });
    }

    return View(userWithRoles);
    }

    // GET: Teachers/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        // var lMSUser = await _userManager.Users
        //     .Include(l => l.AdvisorySection)
        //     .Include(l => l.Subjects)
        //     .FirstOrDefaultAsync(m => m.Id == id);
        var user = await _userManager.Users
            .Where(u => u.Id == id)
            .Select(u => new
            {
                u.Id,
                u.FirstName,
                u.LastName,
                AdvisorySection = u.AdvisorySection == null ? null : new
                {
                    u.AdvisorySection.Id,
                    u.AdvisorySection.Name
                },
                Subjects = (u.Subjects ?? new List<Subject>())
                    .Select(s => new { s.Id, s.Name })
                    .ToList()
            })
            .FirstOrDefaultAsync();
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    // GET: Teachers/Create
    public IActionResult Create()
    {

        // ViewData["AdvisorySectionId"] = new SelectList(_userManager.Sections, "Id", "Id");
        return View();
    }

    // POST: Teachers/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LMSUserCreateViewModel model)
    {
        var user = new LMSUser
        {
            UserName = model.UserName,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Verified = model.Verified,
            AdvisorySectionId = model.AdvisorySectionId,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
            return RedirectToAction("Index");

        foreach (var error in result.Errors)
            ModelState.AddModelError("", error.Description);

        return View(model);

        // ViewData["AdvisorySectionId"] = new SelectList(_userManager.Sections, "Id", "Id", user.AdvisorySectionId);
        // return View(user);
    }

    // GET: Teachers/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var lMSUser = await _userManager.Users
            .Include(l => l.AdvisorySection)
            .Include(l => l.Subjects)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (lMSUser == null)
        {
            return NotFound();
        }
        // ViewData["AdvisorySectionId"] = new SelectList(_userManager.Sections, "Id", "Id", lMSUser.AdvisorySectionId);
        return View(lMSUser);
    }

    // POST: Teachers/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(LMSUserEditViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.Id);
        if (user == null)
            return NotFound();

        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.MiddleName = model.MiddleName;
        user.Verified = model.Verified;
        user.AdvisorySectionId = model.AdvisorySectionId;
        user.Email = model.Email;
        user.UserName = model.UserName;

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
            return RedirectToAction("Index");

        foreach (var error in result.Errors)
            ModelState.AddModelError("", error.Description);
        // ViewData["AdvisorySectionId"] = new SelectList(_userManager.Sections, "Id", "Id", lMSUser.AdvisorySectionId);
        return View(model);
    }

    // GET: Teachers/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var lMSUser = await _userManager.Users
            .Include(l => l.AdvisorySection)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (lMSUser == null)
        {
            return NotFound();
        }

        return View(lMSUser);
    }

    // POST: Teachers/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound();
        var result = await _userManager.DeleteAsync(user);

        if (result.Succeeded)
            return RedirectToAction(nameof(Index));

        foreach (var error in result.Errors)
            ModelState.AddModelError("", error.Description);
        return View("Delete", user); // Optional: return to delete confirmation view
    }

    private bool LMSUserExists(string id)
    {
        return _userManager.Users.Any(e => e.Id == id);
    }
}

