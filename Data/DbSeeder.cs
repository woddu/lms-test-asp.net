using Microsoft.AspNetCore.Identity;
using lms_test1.Areas.Identity.Data;

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
            new { UserName = "admin", Email = "admin@admin.com", FirstName = "System", LastName = "Administrator", Role = "Admin", Password = "@Admin2025" },
            new { UserName = "headteacher1", Email = "headteacher@school.com", FirstName = "Bea Rhumeyla", LastName = "Talion", Role = "HeadTeacher", Password = "@Head2025" },
            new { UserName = "teacher1", Email = "teacher@school.com", FirstName = "Ron Neil", LastName = "Castro", Role = "Teacher", Password = "@Teach2025" }
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
            var subjects = new List<Models.Subject>
        {
            // Core Subjects (All Tracks)
            new Models.Subject { Name = "Oral Communication" },
            new Models.Subject { Name = "Reading and Writing" },
            new Models.Subject { Name = "Komunikasyon sa Wika at Kulturang Pilipino" },
            new Models.Subject { Name = "21st Century Literature" },
            new Models.Subject { Name = "General Mathematics" },
            new Models.Subject { Name = "Earth and Life Science" },
            new Models.Subject { Name = "Understanding Culture, Society and Politics" },
            new Models.Subject { Name = "Media and Information Literacy" },
            new Models.Subject { Name = "Statistics and Probability" },
            new Models.Subject { Name = "Personal Development" },

            // STEM Track
            new Models.Subject { Name = "Pre-Calculus" },
            new Models.Subject { Name = "Basic Calculus" },
            new Models.Subject { Name = "General Biology 1" },
            new Models.Subject { Name = "General Chemistry 1" },
            new Models.Subject { Name = "General Physics 1" },
            new Models.Subject { Name = "General Biology 2" },
            new Models.Subject { Name = "General Chemistry 2" },
            new Models.Subject { Name = "General Physics 2" },

            // ABM Track
            new Models.Subject { Name = "Business Math" },
            new Models.Subject { Name = "Organization and Management" },
            new Models.Subject { Name = "Principles of Marketing" },
            new Models.Subject { Name = "Business Finance" },
            new Models.Subject { Name = "Applied Economics" },
            new Models.Subject { Name = "Business Ethics and Social Responsibility" },

            // HUMSS Track
            new Models.Subject { Name = "Creative Writing" },
            new Models.Subject { Name = "Philippine Politics and Governance" },
            new Models.Subject { Name = "World Religions and Belief Systems" },
            new Models.Subject { Name = "Community Engagement" },
            new Models.Subject { Name = "Disciplines and Ideas in Social Sciences" },

            // TVL Track (sample subjects)
            new Models.Subject { Name = "Bread and Pastry Production" },
            new Models.Subject { Name = "Computer Systems Servicing" },
            new Models.Subject { Name = "Cookery" },

            // Work Immersion / Culminating Activity
            new Models.Subject { Name = "Practical Research 1" },
            new Models.Subject { Name = "Practical Research 2" },
            new Models.Subject { Name = "Work Immersion" },
            new Models.Subject { Name = "Inquiries, Investigations and Immersion" }
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

            // Define strands per track
            var academicStrands = new[] { "STEM", "ABM", "HUMSS", "GAS" };
            var tvlStrands = new[] { "Home Economics", "ICT", "Industrial Arts", "Agri-Fishery Arts" };
            var artsSportsStrands = new[] { "Arts and Design", "Sports" };

            var sections = Enumerable.Range(1, 10).Select(i =>
            {
                var isGrade11 = i % 2 == 0;
                var name = isGrade11
                    ? grade11Names[i / 2 % grade11Names.Length]
                    : grade12Names[i / 2 % grade12Names.Length];

                string track;
                string strand;

                switch (i % 4)
                {
                    case 0:
                        track = "Core Subject (All Tracks)";
                        strand = "N/A"; // Core subjects apply to all strands
                        break;
                    case 1:
                        track = "Academic Track (except Immersion)";
                        strand = academicStrands[i % academicStrands.Length];
                        break;
                    case 2:
                        track = "Work Immersion/ Culminating Activity (for Academic Track)";
                        strand = academicStrands[i % academicStrands.Length];
                        break;
                    default:
                        track = "TVL/ Sports/ Arts and Design Track";
                        // Alternate between TVL and Arts/Sports strands
                        strand = (i % 2 == 0)
                            ? tvlStrands[i % tvlStrands.Length]
                            : artsSportsStrands[i % artsSportsStrands.Length];
                        break;
                }

                return new Models.Section
                {
                    Name = name,
                    Track = track,
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
            var students = Enumerable.Range(1, 10).Select(i => new Models.Student
            {
                LastName = $"LastName {i}",
                FirstName = $"FirstName {i}",
                MiddleName = $"M{i}",
                Gender = (i % 2 == 0) ? 'F' : 'M',
                Age = 13 + (i % 6),
                BirthDate = DateTime.Now.AddYears(-13 - (i % 6)).AddDays(i),
                Address = $"{i} Main St",
                SectionId = null // You can assign SectionId if you want to relate
            }).ToList();
            context.Students.AddRange(students);
            await context.SaveChangesAsync();
        }

    }
}
