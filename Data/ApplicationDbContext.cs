using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using lms_test1.Models;
using lms_test1.Areas.Identity.Data;

namespace lms_test1.Data;

public class ApplicationDbContext : IdentityDbContext<LMSUser>
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Score> Scores { get; set; }
    public DbSet<TeacherSubject> TeacherSubjects { get; set; }
    public DbSet<TeacherSubjectSection> TeacherSubjectSections { get; set; }
    public DbSet<StudentExtraFieldDefinition> StudentsExtraFieldDefinitions { get; set; }
    public DbSet<StudentExtraField> StudentsExtraFields { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Section>()
            .HasOne(s => s.Adviser)
            .WithMany(u => u.AdvisorySections)   // explicitly one-to-many
            .HasForeignKey(s => s.AdviserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Composite key for TeacherSubject -- OLD
        // builder.Entity<TeacherSubject>()
        //     .HasKey(ts => new { ts.TeacherId, ts.SubjectId });
        //     .IsUnique();

        // TeacherSubject → Teacher(LMSUser)
        builder.Entity<TeacherSubject>()
            .HasOne(ts => ts.Teacher)
            .WithMany(t => t.TeacherSubjects)
            .HasForeignKey(ts => ts.TeacherId);

        // TeacherSubject → Subject
        builder.Entity<TeacherSubject>()
            .HasOne(ts => ts.Subject)
            .WithMany(s => s.TeacherSubjects)
            .HasForeignKey(ts => ts.SubjectId);

        builder.Entity<TeacherSubject>()
            .HasIndex(ts => new { ts.TeacherId, ts.SubjectId })
            .IsUnique();

        builder.Entity<TeacherSubjectSection>()
            .HasKey(tss => new { tss.TeacherSubjectId, tss.SectionId });

        builder.Entity<TeacherSubjectSection>()
            .HasOne(tss => tss.TeacherSubject)
            .WithMany(ts => ts.TeacherSubjectSections)
            .HasForeignKey(tss => tss.TeacherSubjectId);

        builder.Entity<TeacherSubjectSection>()
            .HasOne(tss => tss.Section)
            .WithMany(s => s.TeacherSubjectSections)
            .HasForeignKey(tss => tss.SectionId);

        // Enforce uniqueness: one Subject per Section
        builder.Entity<TeacherSubjectSection>()
            .HasIndex(tss => new { tss.SectionId, tss.SubjectId })
            .IsUnique();



        // Score → Student
        builder.Entity<Score>()
            .HasOne(s => s.Student)
            .WithMany(st => st.Scores)
            .HasForeignKey(s => s.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        // Score → TeacherSubject (composite FK)
        builder.Entity<Score>()
            .HasOne(s => s.TeacherSubject)
            .WithMany(ts => ts.Scores)
            .HasForeignKey(s => s.TeacherSubjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<StudentExtraField>()
            .HasOne(sef => sef.Student)
            .WithMany(s => s.ExtraFields)
            .HasForeignKey(sef => sef.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<StudentExtraField>()
            .HasOne(sef => sef.ExtraFieldDefinition)
            .WithMany()
            .HasForeignKey(sef => sef.ExtraFieldDefinitionId);
    }
}
