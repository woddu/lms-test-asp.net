using lms_test1.Areas.Identity.Data;

namespace lms_test1.Models;

public class TeacherSubject
{
    public string TeacherId { get; set; }
    public LMSUser Teacher { get; set; }

    public int SubjectId { get; set; }
    public Subject Subject { get; set; }

    public int? SectionId { get; set; }
    public Section Section { get; set; }

    public ICollection<Score> Scores { get; set; } = new List<Score>();

    public double WW1 { get; set; }
    public double WW2 { get; set; }
    public double WW3 { get; set; }
    public double WW4 { get; set; }
    public double WW5 { get; set; }
    public double WW6 { get; set; }
    public double WW7 { get; set; }
    public double WW8 { get; set; }
    public double WW9 { get; set; }
    public double WW10 { get; set; }

    public double WWTotal
    {
        get
        {
            double total = WW1 + WW2 + WW3 + WW4 + WW5 + WW6 + WW7 + WW8 + WW9 + WW10;
            return total < 1 ? 1 : total;
        }
    }

    // Performance Tasks
    public double PT1 { get; set; }
    public double PT2 { get; set; }
    public double PT3 { get; set; }
    public double PT4 { get; set; }
    public double PT5 { get; set; }
    public double PT6 { get; set; }
    public double PT7 { get; set; }
    public double PT8 { get; set; }
    public double PT9 { get; set; }
    public double PT10 { get; set; }

    public double PTTotal
    {
        get
        {
            double ptTotal = PT1 + PT2 + PT3 + PT4 + PT5 + PT6 + PT7 + PT8 + PT9 + PT10;
            return ptTotal < 1 ? 1 : ptTotal;
        }
    }

    // Exam
    public double Exam { get; set; }

    public TeacherSubject()
    {
        WW1 = WW2 = WW3 = WW4 = WW5 = WW6 = WW7 = WW8 = WW9 = WW10 = 0;
        PT1 = PT2 = PT3 = PT4 = PT5 = PT6 = PT7 = PT8 = PT9 = PT10 = 0;
        Exam = 0;
    }
}


