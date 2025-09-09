// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using lms_test1.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace lms_test1.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        // private readonly SignInManager<LMSUser> _signInManager;
        // private readonly UserManager<LMSUser> _userManager;
        // private readonly IUserStore<LMSUser> _userStore;
        // private readonly IUserEmailStore<LMSUser> _emailStore;
        // private readonly ILogger<RegisterModel> _logger;
        // private readonly IEmailSender _emailSender;

        public IActionResult OnGet()
    {
        return RedirectToPage("Login");
    }

    public IActionResult OnPost()
    {
        return RedirectToPage("Login");
    }
    }
}
