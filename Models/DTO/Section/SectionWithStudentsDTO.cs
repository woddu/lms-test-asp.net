namespace lms_test1.Models.DTO.Section;

public record SectionWithStudentsDTO(
    Models.Section Section,
    List<Student.StudentInListDTO> Students
);