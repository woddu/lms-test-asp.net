using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lms_test1.Models.ViewModels.Teachers;

public class LMSUserEditViewModel
{
    public string Id { get; set; } = string.Empty;

    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;
    
    [Display(Name = "Middle Name")]
    public string? MiddleName { get; set; }
    public bool Verified { get; set; }
    
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "User Name")]
    public string UserName { get; set; } = string.Empty;

    public List<SelectListItem>? AdvisorySectionOptions { get; set; }

    // Optional: for display only, not editable
    public List<TeacherSubjectViewModel>? Subjects { get; set; }
}
