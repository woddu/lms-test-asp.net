namespace lms_test1.Models.DTO.Advisory;

public record SectionWithStudentsWithScoresDTO(
    Models.Section Section,
    List<Models.Student> Students
);