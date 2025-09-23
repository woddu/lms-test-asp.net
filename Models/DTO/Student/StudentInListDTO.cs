namespace lms_test1.Models.DTO.Student;

public record StudentInListDTO(
    int Id,
    string LastName,
    string FirstName,
    string MiddleName = ""
);