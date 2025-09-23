namespace lms_test1.Models.ViewModels.TeachersManagement;

public class UserWithRolesViewModel
{
    public required string Id { get; set; }
    public required string Initials { get; set; }
    public required bool Verified { get; set; }
    public required string Role { get; set; }
}