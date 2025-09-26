using lms_test1.Areas.Identity.Data;

namespace lms_test1.Models;

public class Section
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Strand { get; set; }
    public required int YearLevel { get; set; }
    public string? AdviserId { get; set; }
    public LMSUser? Adviser { get; set; } 
    public ICollection<Student>? Students { get; set; } = new List<Student>();
    public ICollection<TeacherSubjectSection> TeacherSubjectSections { get; set; } = new List<TeacherSubjectSection>();
}