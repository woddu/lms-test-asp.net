namespace lms_test1.Models.ViewModels.Subject;

using lms_test1.Models;
using lms_test1.Models.DTO.Teacher;

public class SubjectDetails
{
    public required Subject Subject { get; set; }
    public List<TeacherInListDTO> Teachers { get; set; } = new();

    public List<Section> Sections { get; set; } = new();
}