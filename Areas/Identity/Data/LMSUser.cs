using Microsoft.AspNetCore.Identity;
using lms_test1.Models;

namespace lms_test1.Areas.Identity.Data;

public class LMSUser : IdentityUser
{
    public bool Verified { get; set; } = false;
    [PersonalData]
    public required string LastName { get; set; }
    [PersonalData]
    public string? MiddleName { get; set; }
    [PersonalData]
    public required string FirstName { get; set; }

    public ICollection<Subject>? Subjects { get; set; }
    public int? AdvisorySectionId { get; set; }
    public Section? AdvisorySection { get; set; }
}