namespace lms_test1.Models.DTO.Student;

public record StudentInListDTO(
    int Id,
    string LastName,
    string FirstName,
    char Gender,
    string MiddleName = ""
);