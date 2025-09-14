using Microsoft.AspNetCore.Identity;
using lms_test1.Areas.Identity.Data;
using lms_test1.Models;

namespace lms_test1.Data;

public static class DbSeeder
{
    public static async Task SeedRolesAndUsersAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<LMSUser>>();

        // 1. Seed roles
        string[] roleNames = ["Admin", "HeadTeacher", "Teacher"];
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // 2. Define users to seed
        var usersToSeed = new[]
        {
            new { UserName = "admin", Email = "admin@admin.com", FirstName = "System", MiddleName="Admin", LastName = "Administrator", Role = "Admin", Password = "@Admin2025" },
            new { UserName = "headteacher1", Email = "headteacher@school.com", FirstName = "Bea Rhumeyla", MiddleName="Sejera", LastName = "Talion", Role = "HeadTeacher", Password = "@Head2025" },
            new { UserName = "teacher1", Email = "teacher@school.com", FirstName = "Ron Neil", MiddleName="Navea", LastName = "Castro", Role = "Teacher", Password = "@Teach2025" },
            new { UserName = "mtdelacruz", Email = "mtdelacruz@school.edu.ph", FirstName = "Maria", MiddleName = "Teresa", LastName = "Dela Cruz", Role = "Teacher", Password = "@Teach2025"  },
            new { UserName = "jrsantos", Email = "jrsantos@school.edu.ph", FirstName = "Jose", MiddleName = "Ramon", LastName = "Santos", Role = "Teacher", Password = "@Teach2025"  },
            new { UserName = "albautista", Email = "albautista@school.edu.ph", FirstName = "Ana", MiddleName = "Lourdes", LastName = "Bautista", Role = "Teacher", Password = "@Teach2025"  },
            new { UserName = "rvreyes", Email = "rvreyes@school.edu.ph", FirstName = "Roberto", MiddleName = "Villanueva", LastName = "Reyes", Role = "Teacher", Password = "@Teach2025"  },
            new { UserName = "lgmendoza", Email = "lgmendoza@school.edu.ph", FirstName = "Lourdes", MiddleName = "Garcia", LastName = "Mendoza", Role = "Teacher", Password = "@Teach2025"  },
            new { UserName = "ecfernandez", Email = "ecfernandez@school.edu.ph", FirstName = "Ernesto", MiddleName = "Cruz", LastName = "Fernandez", Role = "Teacher", Password = "@Teach2025"  },
            new { UserName = "craquino", Email = "craquino@school.edu.ph", FirstName = "Cecilia", MiddleName = "Ramos", LastName = "Aquino", Role = "Teacher", Password = "@Teach2025"  },
            new { UserName = "mlnavarro", Email = "mlnavarro@school.edu.ph", FirstName = "Manuel", MiddleName = "Lopez", LastName = "Navarro", Role = "Teacher", Password = "@Teach2025"  },
            new { UserName = "tvcastillo", Email = "tvcastillo@school.edu.ph", FirstName = "Teresa", MiddleName = "Villamor", LastName = "Castillo", Role = "Teacher", Password = "@Teach2025"  },
            new { UserName = "asvillanueva", Email = "asvillanueva@school.edu.ph", FirstName = "Alfredo", MiddleName = "Santos", LastName = "Villanueva", Role = "Teacher", Password = "@Teach2025"  }
        };

        // 3. Create each user if missing
        foreach (var u in usersToSeed)
        {
            var existingUser = await userManager.FindByEmailAsync(u.Email);
            if (existingUser == null)
            {
                var newUser = new LMSUser
                {
                    UserName = u.UserName,
                    Email = u.Email,
                    EmailConfirmed = true,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Verified = true
                };

                var result = await userManager.CreateAsync(newUser, u.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, u.Role);
                }
                else
                {
                    throw new Exception($"Failed to create user {u.Email}: {string.Join(", ", result.Errors)}");
                }
            }
        }
    }

    public static async Task SeedEntitiesAsync(ApplicationDbContext context)
    {
        // Seed 10 Subjects
        if (!context.Subjects.Any())
        {
            // context.Subjects.RemoveRange(context.Subjects);
            // await context.SaveChangesAsync();
            var subjects = new List<Subject>
            {
                // Core Subjects (All Tracks)
                new Subject { Name = "Oral Communication", Track = "Core Subject (All Tracks)" },
                new Subject { Name = "Reading and Writing", Track = "Core Subject (All Tracks)" },
                new Subject { Name = "Komunikasyon sa Wika at Kulturang Pilipino", Track = "Core Subject (All Tracks)" },
                new Subject { Name = "21st Century Literature", Track = "Core Subject (All Tracks)" },
                new Subject { Name = "General Mathematics", Track = "Core Subject (All Tracks)" },
                new Subject { Name = "Earth and Life Science", Track = "Core Subject (All Tracks)" },
                new Subject { Name = "Understanding Culture, Society and Politics", Track = "Core Subject (All Tracks)" },
                new Subject { Name = "Media and Information Literacy", Track = "Core Subject (All Tracks)" },
                new Subject { Name = "Statistics and Probability", Track = "Core Subject (All Tracks)" },
                new Subject { Name = "Personal Development", Track = "Core Subject (All Tracks)" },

                // STEM Track (Academic)
                new Subject { Name = "Pre-Calculus", Track = "Academic Track (except Immersion)" },
                new Subject { Name = "Basic Calculus", Track = "Academic Track (except Immersion)" },
                new Subject { Name = "General Biology 1", Track = "Academic Track (except Immersion)" },
                new Subject { Name = "General Chemistry 1", Track = "Academic Track (except Immersion)" },
                new Subject { Name = "General Physics 1", Track = "Academic Track (except Immersion)" },
                new Subject { Name = "General Biology 2", Track = "Academic Track (except Immersion)" },
                new Subject { Name = "General Chemistry 2", Track = "Academic Track (except Immersion)" },
                new Subject { Name = "General Physics 2", Track = "Academic Track (except Immersion)" },

                // ABM Track (Academic)
                new Subject { Name = "Business Math", Track = "Academic Track (except Immersion)" },
                new Subject { Name = "Organization and Management", Track = "Academic Track (except Immersion)" },
                new Subject { Name = "Principles of Marketing", Track = "Academic Track (except Immersion)" },
                new Subject { Name = "Business Finance", Track = "Academic Track (except Immersion)" },
                new Subject { Name = "Applied Economics", Track = "Academic Track (except Immersion)" },
                new Subject { Name = "Business Ethics and Social Responsibility", Track = "Academic Track (except Immersion)" },

                // HUMSS Track (Academic)
                new Subject { Name = "Creative Writing", Track = "Academic Track (except Immersion)" },
                new Subject { Name = "Philippine Politics and Governance", Track = "Academic Track (except Immersion)" },
                new Subject { Name = "World Religions and Belief Systems", Track = "Academic Track (except Immersion)" },
                new Subject { Name = "Community Engagement", Track = "Academic Track (except Immersion)" },
                new Subject { Name = "Disciplines and Ideas in Social Sciences", Track = "Academic Track (except Immersion)" },

                // TVL / Sports / Arts and Design Track
                new Subject { Name = "Bread and Pastry Production", Track = "TVL/ Sports/ Arts and Design Track" },
                new Subject { Name = "Computer Systems Servicing", Track = "TVL/ Sports/ Arts and Design Track" },
                new Subject { Name = "Cookery", Track = "TVL/ Sports/ Arts and Design Track" },

                // Work Immersion / Culminating Activity (Academic)
                new Subject { Name = "Practical Research 1", Track = "Work Immersion/ Culminating Activity (for Academic Track)" },
                new Subject { Name = "Practical Research 2", Track = "Work Immersion/ Culminating Activity (for Academic Track)" },
                new Subject { Name = "Work Immersion", Track = "Work Immersion/ Culminating Activity (for Academic Track)" },
                new Subject { Name = "Inquiries, Investigations and Immersion", Track = "Work Immersion/ Culminating Activity (for Academic Track)" }
            };


            context.Subjects.AddRange(subjects);
            await context.SaveChangesAsync();
        }



        // Seed 10 Sections
        if (!context.Sections.Any())
        {
            // context.Sections.RemoveRange(context.Sections);
            // await context.SaveChangesAsync();
            var grade11Names = new[] { "Orion", "Cassiopeia", "Lyra", "Andromeda", "Pegasus" };
            var grade12Names = new[] { "Socrates", "Plato", "Aristotle", "Pythagoras", "Epicurus" };

            // Define strands
            var academicStrands = new[] { "STEM", "ABM", "HUMSS", "GAS" };
            var tvlStrands = new[] { "Home Economics", "ICT", "Industrial Arts", "Agri-Fishery Arts" };
            var artsSportsStrands = new[] { "Arts and Design", "Sports" };

            var sections = Enumerable.Range(1, 10).Select(i =>
            {
                var isGrade11 = i % 2 == 0;
                var name = isGrade11
                    ? grade11Names[i / 2 % grade11Names.Length]
                    : grade12Names[i / 2 % grade12Names.Length];

                string strand;

                switch (i % 3) // rotate between Academic, TVL, Arts/Sports
                {
                    case 0:
                        strand = academicStrands[i % academicStrands.Length];
                        break;
                    case 1:
                        strand = tvlStrands[i % tvlStrands.Length];
                        break;
                    default:
                        strand = artsSportsStrands[i % artsSportsStrands.Length];
                        break;
                }

                return new Section
                {
                    Name = name,
                    YearLevel = isGrade11 ? 11 : 12,
                    Strand = strand
                };
            }).ToList();


            context.Sections.AddRange(sections);
            await context.SaveChangesAsync();

        }


        // Seed 10 Students
        if (!context.Students.Any())
        {
            // context.Students.RemoveRange(context.Students);
            // await context.SaveChangesAsync();
            var students = new List<Student>
            {
                new Student { FirstName = "Juan", MiddleName = "Miguel", LastName = "Cruz", Gender = 'M' },
                new Student { FirstName = "Maria", MiddleName = "Isabel", LastName = "Reyes", Gender = 'F' },
                new Student { FirstName = "Carlo", MiddleName = "Antonio", LastName = "Santos", Gender = 'M' },
                new Student { FirstName = "Angela", MiddleName = "Marie", LastName = "Dela Cruz", Gender = 'F' },
                new Student { FirstName = "Paolo", MiddleName = "Jose", LastName = "Mendoza", Gender = 'M' },
                new Student { FirstName = "Kristine", MiddleName = "Anne", LastName = "Bautista", Gender = 'F' },
                new Student { FirstName = "Mark", MiddleName = "Anthony", LastName = "Villanueva", Gender = 'M' },
                new Student { FirstName = "Camille", MiddleName = "Rose", LastName = "Navarro", Gender = 'F' },
                new Student { FirstName = "Joshua", MiddleName = "David", LastName = "Fernandez", Gender = 'M' },
                new Student { FirstName = "Patricia", MiddleName = "Mae", LastName = "Aquino", Gender = 'F' },
                new Student { FirstName = "Adrian", MiddleName = "Paul", LastName = "Garcia", Gender = 'M' },
                new Student { FirstName = "Nicole", MiddleName = "Joy", LastName = "Castillo", Gender = 'F' },
                new Student { FirstName = "Francis", MiddleName = "Leo", LastName = "Ramos", Gender = 'M' },
                new Student { FirstName = "Denise", MiddleName = "Claire", LastName = "Lopez", Gender = 'F' },
                new Student { FirstName = "Kevin", MiddleName = "John", LastName = "Villamor", Gender = 'M' },
                new Student { FirstName = "Shaira", MiddleName = "Lyn", LastName = "Cruz", Gender = 'F' },
                new Student { FirstName = "Jerome", MiddleName = "Patrick", LastName = "Reyes", Gender = 'M' },
                new Student { FirstName = "Hannah", MiddleName = "Mae", LastName = "Santos", Gender = 'F' },
                new Student { FirstName = "Vincent", MiddleName = "Carlo", LastName = "Dela Cruz", Gender = 'M' },
                new Student { FirstName = "Alyssa", MiddleName = "Marie", LastName = "Mendoza", Gender = 'F' },
                new Student { FirstName = "Gabriel", MiddleName = "Luis", LastName = "Bautista", Gender = 'M' },
                new Student { FirstName = "Charlene", MiddleName = "Faith", LastName = "Villanueva", Gender = 'F' },
                new Student { FirstName = "Raymond", MiddleName = "Paul", LastName = "Navarro", Gender = 'M' },
                new Student { FirstName = "Erika", MiddleName = "Jane", LastName = "Fernandez", Gender = 'F' },
                new Student { FirstName = "Daniel", MiddleName = "James", LastName = "Aquino", Gender = 'M' }
            };
            context.Students.AddRange(students);
            await context.SaveChangesAsync();
        }
    }
}
