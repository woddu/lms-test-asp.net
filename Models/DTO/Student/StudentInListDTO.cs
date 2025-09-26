namespace lms_test1.Models.DTO.Student;

public record StudentInListDTO(
    int Id,
    string FirstName,
    string LastName,
    char Gender,
    string MiddleName = ""
);