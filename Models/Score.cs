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
            
            double wwTotal = 0;
            double ptTotal = 0;
            
            for (int i = 1; i <= 10; i++)
            {
                foreach (var prop in TeacherSubject.GetType().GetProperties())
                {
                    if (prop.Name == $"WW{i}_First" && (double)prop.GetValue(TeacherSubject)! != 0)
                    {
                        foreach (var sProp in this.GetType().GetProperties())
                        {
                            if (sProp.Name == $"WW{i}_First" && (double?)sProp.GetValue(this) != 0)
                            {
                                wwTotal += (double)sProp.GetValue(this)!;
                            }
                        }
                    }
                    if (prop.Name == $"PT{i}_First" && (double)prop.GetValue(TeacherSubject)! != 0)
                    {
                        foreach (var sProp in this.GetType().GetProperties())
                        {
                            if (sProp.Name == $"PT{i}_First" && (double)sProp.GetValue(this)! != 0)
                            {
                                ptTotal += (double)sProp.GetValue(this)!;
                            }
                        }
                    }
                }
            }  

            double wwWeighted = wwTotal / TeacherSubject.WWTotal_First * 100 * WWPercentage;
            double ptWeighted = ptTotal / TeacherSubject.PTTotal_First * 100 * PTPercentage;
            double examWeighted = TeacherSubject.Exam_First > 0
                ? Exam_First / TeacherSubject.Exam_First * 100 * ExamPercentage
                : 0;

            double initialGrade = wwWeighted + ptWeighted + examWeighted;

            return initialGrade;

        }

    }
    public double InitialGrade_Second
    {
        get
        {
            
            double wwTotal = 0;
            double ptTotal = 0;
            
            for (int i = 1; i <= 10; i++)
            {
                foreach (var prop in TeacherSubject.GetType().GetProperties())
                {
                    if (prop.Name == $"WW{i}_Second" && (double)prop.GetValue(TeacherSubject)! != 0)
                    {
                        foreach (var sProp in this.GetType().GetProperties())
                        {
                            if (sProp.Name == $"WW{i}_Second" && (double?)sProp.GetValue(this) != 0)
                            {
                                wwTotal += (double)sProp.GetValue(this)!;
                            }
                        }
                    }
                    if (prop.Name == $"PT{i}_Second" && (double)prop.GetValue(TeacherSubject)! != 0)
                    {
                        foreach (var sProp in this.GetType().GetProperties())
                        {
                            if (sProp.Name == $"PT{i}_Second" && (double)sProp.GetValue(this)! != 0)
                            {
                                ptTotal += (double)sProp.GetValue(this)!;
                            }
                        }
                    }
                }
            }  

            double wwWeighted = wwTotal / TeacherSubject.WWTotal_Second * 100 * WWPercentage;
            double ptWeighted = ptTotal / TeacherSubject.PTTotal_Second * 100 * PTPercentage;
            double examWeighted = TeacherSubject.Exam_Second > 0
                ? Exam_Second / TeacherSubject.Exam_Second * 100 * ExamPercentage
                : 0;

            double initialGrade = wwWeighted + ptWeighted + examWeighted;

            return initialGrade;

        }
    }

    public double FinalGrade_First { get; set; } = 0;
    public double FinalGrade_Second { get; set; } = 0;

}
