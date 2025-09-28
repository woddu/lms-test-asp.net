using System.Text.Json.Serialization;
using lms_test1.Areas.Identity.Data;

namespace lms_test1.Models;

public class Score
{
    public int Id { get; set; }

    // Relationships
    public required int StudentId { get; set; }
    public required Student Student { get; set; }

    public required int TeacherSubjectId { get; set; }
    [JsonIgnore]
    public TeacherSubject TeacherSubject { get; set; }

    // Written Works
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

    public double WWPercentage
    {
        get
        {
            //         "Core Subject (All Tracks)",
            // "Academic Track (except Immersion)",
            // "Work Immersion/ Culminating Activity (for Academic Track)",
            // "TVL/ Sports/ Arts and Design Track"
            if (TeacherSubject.Subject?.Track.Trim() == "Core Subject (All Tracks)")
            {
                return 0.25; // 40% for Core Subject (All Tracks)
            }
            else if (TeacherSubject.Subject?.Track.Trim() == "Academic Track (except Immersion)")
            {
                return 0.25; // 35% for Academic Track (except Immersion)
            }
            else if (TeacherSubject.Subject?.Track.Trim() == "Work Immersion/ Culminating Activity (for Academic Track)")
            {
                return 0.35; // 25% for Work Immersion/ Culminating Activity (for Academic Track)
            }
            else if (TeacherSubject.Subject?.Track.Trim() == "TVL/ Sports/ Arts and Design Track")
            {
                return 0.20; // 30% for TVL/ Sports/ Arts and Design Track
            }
            else
            {
                return 0.25; // Default to 25%
            }
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

    public double PTPercentage
    {
        get
        {
            if (TeacherSubject.Subject?.Track.Trim() == "Core Subject (All Tracks)")
            {
                return 0.50; // 50% for Core Subject (All Tracks)
            }
            else if (TeacherSubject.Subject?.Track.Trim() == "Academic Track (except Immersion)")
            {
                return 0.45; // 50% for Academic Track (except Immersion)
            }
            else if (TeacherSubject.Subject?.Track.Trim() == "Work Immersion/ Culminating Activity (for Academic Track)")
            {
                return 0.40; // 50% for Work Immersion/ Culminating Activity (for Academic Track)
            }
            else if (TeacherSubject.Subject?.Track.Trim() == "TVL/ Sports/ Arts and Design Track")
            {
                return 0.60; // 50% for TVL/ Sports/ Arts and Design Track
            }
            else
            {
                return 0.50; // Default to 50%
            }
        }
    }

    // Exam
    public double Exam_First { get; set; } = 0;

    public double Exam_Second { get; set; } = 0;

    public double ExamPercentage
    {
        get
        {
            if (TeacherSubject.Subject?.Track.Trim() == "Core Subject (All Tracks)")
            {
                return 0.25; // 25% for Core Subject (All Tracks)
            }
            else if (TeacherSubject.Subject?.Track.Trim() == "Academic Track (except Immersion)")
            {
                return 0.30; // 30% for Academic Track (except Immersion)
            }
            else if (TeacherSubject.Subject?.Track.Trim() == "Work Immersion/ Culminating Activity (for Academic Track)")
            {
                return 0.25; // 25% for Work Immersion/ Culminating Activity (for Academic Track)
            }
            else if (TeacherSubject.Subject?.Track.Trim() == "TVL/ Sports/ Arts and Design Track")
            {
                return 0.20; // 20% for TVL/ Sports/ Arts and Design Track
            }
            else
            {
                return 0.25; // Default to 25%
            }
        }
    }

    public double InitialGrade_First
    {
        get
        {
            double wwTotal = WW1_First + WW2_First + WW3_First + WW4_First + WW5_First + WW6_First + WW7_First + WW8_First + WW9_First + WW10_First;
            double ptTotal = PT1_First + PT2_First + PT3_First + PT4_First + PT5_First + PT6_First + PT7_First + PT8_First + PT9_First + PT10_First;

            double wwWeighted = wwTotal / (TeacherSubject?.WWTotal_First ?? 1) * 100 / WWPercentage;
            double ptWeighted = ptTotal / (TeacherSubject?.PTTotal_First ?? 1) * 100 / PTPercentage;
            double examTotal = TeacherSubject?.Exam_First ?? 1;
            double examWeighted = Exam_First / (examTotal < 1 ? 1 : examTotal) * 100 / ExamPercentage;

            double initialGrade = wwWeighted + ptWeighted + examWeighted;

            return initialGrade;

        }

    }
    public double InitialGrade_Second
    {
        get
        {
            double wwTotal = WW1_Second + WW2_Second + WW3_Second + WW4_Second + WW5_Second + WW6_Second + WW7_Second + WW8_Second + WW9_Second + WW10_Second;
            double ptTotal = PT1_Second + PT2_Second + PT3_Second + PT4_Second + PT5_Second + PT6_Second + PT7_Second + PT8_Second + PT9_Second + PT10_Second;

            double wwWeighted = wwTotal / (TeacherSubject?.WWTotal_Second ?? 1) * 100 / WWPercentage;
            double ptWeighted = ptTotal / (TeacherSubject?.PTTotal_Second ?? 1) * 100 / PTPercentage;
            double examTotal = TeacherSubject?.Exam_Second ?? 1;
            double examWeighted = Exam_Second / (examTotal < 1 ? 1 : examTotal) * 100 / ExamPercentage;

            double initialGrade = wwWeighted + ptWeighted + examWeighted;

            return initialGrade;

        }
    }

    public double FinalGrade_First { get; set; } = 0;
    public double FinalGrade_Second { get; set; } = 0;

}
