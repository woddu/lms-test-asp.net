using lms_test1.Models.DTO.Section;

namespace lms_test1.Models.ViewModels.TeachersManagement;

public class AssignTeacherViewModel

{
    public string TeacherId { get; set; } = string.Empty;

    public string TeacherName { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public List<string?> Roles { get; set; } = new List<string?>();

    public List<int> AdvisorySectionIds { get; set; } = new List<int>();

    public List<int> SelectedSubjectIds { get; set; } = new List<int>();

    public List<SubjectSectionSelection> SubjectSections { get; set; } = new();
}

public class AssignTeacherViewModelA

{
    public string TeacherId { get; set; } = string.Empty;

    public string TeacherName { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public List<string?> Roles { get; set; } = new List<string?>();

    public List<Section> AdvisorySections { get; set; }

    public List<SubjectWithSections> SubjectWithSections { get; set; }

    public List<IGrouping<int, Section>>? GroupedSections { get; set; }
    public List<IGrouping<string, Models.Subject>>? GroupedSubjects { get; set; }

    public Dictionary<int, List<SectionDTO>>? SubjectSectionOptions { get; set; }
}   

public class SubjectWithSections
{
    public Models.Subject Subject { get; set; }
    public List<Section> Sections { get; set; }
}

public class SubjectSectionSelection
{
    public int SubjectId { get; set; } = 0;
    public List<int> SectionIds { get; set; } = new();
}