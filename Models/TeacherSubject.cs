using System.Text.Json.Serialization;
using lms_test1.Areas.Identity.Data;

namespace lms_test1.Models;

public class TeacherSubject
{
    public int Id { get; set; }

    public string TeacherId { get; set; }
    [JsonIgnore]
    public LMSUser Teacher { get; set; }

    public int SubjectId { get; set; }
    [JsonIgnore]
    public Subject Subject { get; set; }

    [JsonIgnore]
    public ICollection<Section> Sections { get; set; }

    public ICollection<Score> Scores { get; set; } = new List<Score>();

    public double WW1_First { get; set; } = 0;
    public double WW2_First { get; set; } = 0;
    public double WW3_First { get; set; } = 0;
    public double WW4_First { get; set; } = 0;
    public double WW5_First { get; set; } = 0;
    public double WW6_First { get; set; } = 0;
    public double WW7_First { get; set; } = 0;
    public double WW8_First { get; set; } = 0;
    public double WW9_First { get; set; } = 0;
    public double WW10_First { get; set; } = 0;

    public double WWTotal_First
    {
        get
        {
            double total = WW1_First + WW2_First + WW3_First + WW4_First + WW5_First + WW6_First + WW7_First + WW8_First + WW9_First + WW10_First;
            return total < 1 ? 1 : total;
        }
    }

    // Performance Tasks
    public double PT1_First { get; set; } = 0;
    public double PT2_First { get; set; } = 0;
    public double PT3_First { get; set; } = 0;
    public double PT4_First { get; set; } = 0;
    public double PT5_First { get; set; } = 0;
    public double PT6_First { get; set; } = 0;
    public double PT7_First { get; set; } = 0;
    public double PT8_First { get; set; } = 0;
    public double PT9_First { get; set; } = 0;
    public double PT10_First { get; set; } = 0;

    public double PTTotal_First
    {
        get
        {
            double ptTotal = PT1_First + PT2_First + PT3_First + PT4_First + PT5_First + PT6_First + PT7_First + PT8_First + PT9_First + PT10_First;
            return ptTotal < 1 ? 1 : ptTotal;
        }
    }

    // Exam
    public double Exam_First { get; set; } = 0;

    public double WW1_Second { get; set; } = 0;
    public double WW2_Second { get; set; } = 0;
    public double WW3_Second { get; set; } = 0;
    public double WW4_Second { get; set; } = 0;
    public double WW5_Second { get; set; } = 0;
    public double WW6_Second { get; set; } = 0;
    public double WW7_Second { get; set; } = 0;
    public double WW8_Second { get; set; } = 0;
    public double WW9_Second { get; set; } = 0;
    public double WW10_Second { get; set; } = 0;
    public double WWTotal_Second
    {
        get
        {
            double total = WW1_Second + WW2_Second + WW3_Second + WW4_Second + WW5_Second + WW6_Second + WW7_Second + WW8_Second + WW9_Second + WW10_Second;
            return total < 1 ? 1 : total;
        }
    }
    public double PT1_Second { get; set; } = 0;
    public double PT2_Second { get; set; } = 0;
    public double PT3_Second { get; set; } = 0;
    public double PT4_Second { get; set; } = 0;
    public double PT5_Second { get; set; } = 0;
    public double PT6_Second { get; set; } = 0;
    public double PT7_Second { get; set; } = 0;
    public double PT8_Second { get; set; } = 0;
    public double PT9_Second { get; set; } = 0;
    public double PT10_Second { get; set; } = 0;
    public double PTTotal_Second
    {
        get
        {
            double ptTotal = PT1_Second + PT2_Second + PT3_Second + PT4_Second + PT5_Second + PT6_Second + PT7_Second + PT8_Second + PT9_Second + PT10_Second;
            return ptTotal < 1 ? 1 : ptTotal;
        }
    }
    public double Exam_Second { get; set; } = 0;

}


