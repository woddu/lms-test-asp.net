using lms_test1.Areas.Identity.Data;

namespace lms_test1.Models;

public class Subject
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Track { get; set; }
    public ICollection<TeacherSubject>? TeacherSubjects { get; set; }

}