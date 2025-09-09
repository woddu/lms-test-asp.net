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
}
