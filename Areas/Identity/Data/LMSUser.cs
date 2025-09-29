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

    public ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
    //TODO turn to one to many relation where one teacher can have manyZz advisory section
    public IEnumerable<Section>? AdvisorySections { get; set; }
}