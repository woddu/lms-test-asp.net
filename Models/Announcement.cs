namespace lms_test1.Models;

public class Announcement
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public DateTime DatePosted { get; set; } = DateTime.Now;
}