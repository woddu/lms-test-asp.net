namespace lms_test1.Models;

public class StudentExtraField
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public Student Student { get; set; }
    public required string FieldValue { get; set; }

    public int ExtraFieldDefinitionId { get; set; }
    public StudentExtraFieldDefinition ExtraFieldDefinition { get; set; }
}