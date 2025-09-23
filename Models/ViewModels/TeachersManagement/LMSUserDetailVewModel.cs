using System.ComponentModel.DataAnnotations;

namespace lms_test1.Models.ViewModels.Teachers;

public class LMSUserDetailViewModel
{
    public string Id { get; set; } = string.Empty;

    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;
    
    [Display(Name = "Middle Name")]
    public string? MiddleName { get; set; }
    public bool Verified { get; set; }

    [Display(Name = "Advisory Section")]    
    public int? AdvisorySectionId { get; set; }
    
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "User Name")]
    public string UserName { get; set; } = string.Empty;

    public Section? AdvisorySection { get; set; }

     // New property for TeacherSubjects and their Sections
    public List<TeacherSubjectDetail> TeacherSubjects { get; set; } = new();
}

public class TeacherSubjectDetail
{
    public string SubjectName { get; set; } = string.Empty;
    public List<string> SectionNames { get; set; } = new();
}