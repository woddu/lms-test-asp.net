using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace lms_test1.Models;

public class Student
{
    public int Id { get; set; }
    [Required]
    public required string LastName { get; set; }
    [Required]
    public required string FirstName { get; set; }
    [Required]
    public required string MiddleName { get; set; }
    [Required]
    public required char Gender { get; set; }
    public int? Age { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Address { get; set; }
    
    public int? SectionId { get; set; }
    [JsonIgnore]
    public Section? Section { get; set; }
    [JsonIgnore]
    public ICollection<Score>? Scores { get; set; } = new List<Score>();
}