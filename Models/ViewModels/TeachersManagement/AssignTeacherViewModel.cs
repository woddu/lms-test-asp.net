namespace lms_test1.Models.ViewModels.Teachers;

public class AssignTeacherViewModel

{
    public string TeacherId { get; set; } = string.Empty;

    public string TeacherName { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public List<string?> Roles { get; set; } = new List<string?>();

    public int? AdvisorySectionId { get; set; }

    public List<int> SelectedSubjectIds { get; set; } = new List<int>();

    public List<SubjectSectionSelection> SubjectSections { get; set; } = new();
}

public class SubjectSectionSelection
{
    public int SubjectId { get; set; }
    public List<int> SectionIds { get; set; } = new();
}