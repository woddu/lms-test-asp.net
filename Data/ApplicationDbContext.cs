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

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
