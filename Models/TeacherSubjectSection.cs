using System.Text.Json.Serialization;

namespace lms_test1.Models;

public class TeacherSubjectSection
{
    public int TeacherSubjectId { get; set; }
    [JsonIgnore]
    public TeacherSubject TeacherSubject { get; set; }

    public int SectionId { get; set; }
    [JsonIgnore]
    public Section Section { get; set; }

    public int SubjectId { get; set; }
}
