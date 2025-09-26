using lms_test1.Models.DTO.Student;

namespace lms_test1.Models.ViewModels.Grade;

public class StudentScoresViewModel
{
    public required TeacherSubject TeacherSubject { get; set; }
    public required StudentInListDTO Student { get; set; }
    public required Score StudentScore { get; set; }
}