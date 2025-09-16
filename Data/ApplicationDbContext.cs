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

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

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

        // TeacherSubject <-> Section
        builder.Entity<TeacherSubject>()
            .HasMany(ts => ts.Sections)
            .WithMany(s => s.TeacherSubjects)
            .UsingEntity(j => j.ToTable("TeacherSubjectSections"));


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
    }
}
