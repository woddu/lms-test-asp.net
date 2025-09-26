namespace lms_test1.Models.DTO.TeacherSubject;

public record TeacherSubjectWithStudentsDTO(
    Models.TeacherSubject TeacherSubject,
    List<Student.StudentInListDTO> Students
);