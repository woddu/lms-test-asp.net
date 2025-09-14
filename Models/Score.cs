using lms_test1.Areas.Identity.Data;

namespace lms_test1.Models;

public class Score
{
    public int Id { get; set; }
    
    // Relationships
    public required int StudentId { get; set; }
    public required Student Student { get; set; }

    public required int TeacherSubjectId { get; set; } 
    public required TeacherSubject TeacherSubject { get; set; }

    // Written Works
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
    public double Exam { get; set; }

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

    public double InitialGrade
    {
        get
        {
            double wwTotal = WW1 + WW2 + WW3 + WW4 + WW5 + WW6 + WW7 + WW8 + WW9 + WW10;
            double ptTotal = PT1 + PT2 + PT3 + PT4 + PT5 + PT6 + PT7 + PT8 + PT9 + PT10;

            double wwWeighted = wwTotal / (TeacherSubject?.WWTotal ?? 1) * 100 / WWPercentage;
            double ptWeighted = ptTotal / (TeacherSubject?.PTTotal ?? 1) * 100 / PTPercentage;
            double examTotal = TeacherSubject?.Exam ?? 1;
            double examWeighted = Exam / (examTotal < 1 ? 1 : examTotal) * 100 / ExamPercentage;

            double initialGrade = wwWeighted + ptWeighted + examWeighted;

            return initialGrade;

        }
    }

    public Score()
    {
        WW1 = WW2 = WW3 = WW4 = WW5 = WW6 = WW7 = WW8 = WW9 = WW10 = 0;
        PT1 = PT2 = PT3 = PT4 = PT5 = PT6 = PT7 = PT8 = PT9 = PT10 = 0;
        Exam = 0;
    }
}
