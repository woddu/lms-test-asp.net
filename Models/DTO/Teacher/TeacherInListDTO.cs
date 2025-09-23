namespace lms_test1.Models.DTO.Teacher;

public record TeacherInListDTO(
    string Id,
    string LastName,
    string FirstName,
    string MiddleName = ""
);