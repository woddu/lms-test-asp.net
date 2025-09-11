using lms_test1.Areas.Identity.Data;

namespace lms_test1.Models;

public class Section
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Track { get; set; }
    public required int YearLevel { get; set; }
    public LMSUser? Adviser { get; set; } 
    public ICollection<Student>? Students { get; set; }
    public ICollection<TeacherSubject>? TeacherSubjects { get; set; } = new List<TeacherSubject>(); 
}