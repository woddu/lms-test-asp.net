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
public class TeachersController : Controller
{
    private readonly UserManager<LMSUser> _userManager;
    private readonly ApplicationDbContext _context;

    public TeachersController(UserManager<LMSUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

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
            .Select(u => new LMSUserDetailViewModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                AdvisorySection = u.AdvisorySection,
                Subjects = u.Subjects
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
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var lMSUser = await _userManager.Users
            .Include(u => u.AdvisorySection)
            .Include(u => u.Subjects)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (lMSUser == null)
        {
            return NotFound();
        }

        var viewModel = new LMSUserEditViewModel
        {
            Id = lMSUser.Id,
            Verified = lMSUser.Verified,
            FirstName = lMSUser.FirstName,
            MiddleName = lMSUser.MiddleName,
            LastName = lMSUser.LastName,
            Email = lMSUser.Email ?? string.Empty,
            UserName = lMSUser.UserName ?? string.Empty,
            AdvisorySectionId = lMSUser.AdvisorySectionId,
            AdvisorySectionOptions = await _context.Sections
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                })
                .ToListAsync(),
            Subjects = lMSUser.Subjects?
                .Select(s => new TeacherSubjectViewModel
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToList()
        };

        return View(viewModel);
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

