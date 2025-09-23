namespace lms_test1.Models.ViewModels.Sections;

using lms_test1.Models.DTO.Student;
using lms_test1.Models;

public class SectionDetails
{
    public Section Section { get; set; } = null!;
    public List<StudentInListDTO> Students { get; set; } = new();

    public List<Subject> Subjects { get; set; } = new();
}   