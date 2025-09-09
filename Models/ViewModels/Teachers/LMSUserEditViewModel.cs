namespace lms_test1.Models.ViewModels.Teachers;

public class LMSUserEditViewModel
{
    public string Id { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public bool Verified { get; set; }
    public int? AdvisorySectionId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
}
