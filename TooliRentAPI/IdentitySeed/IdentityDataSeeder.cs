using Microsoft.AspNetCore.Identity;

namespace Presentation.IdentitySeed
{
    public static class IdentityDataSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Create roles
            string[] roles = { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create Admin user
            var adminEmail = config.GetSection("SeedUser:email").Get<string>();
            var adminUsername = config.GetSection("SeedUser:username").Get<string>();
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminUsername,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var create = await userManager.CreateAsync(adminUser, config.GetSection("SeedUser:password").Get<string>());
                if (!create.Succeeded)
                {
                    var errors = string.Join("; ", create.Errors.Select(e => $"{e.Code}:{e.Description}"));
                    throw new Exception($"Failed to create seed admin: {errors}");
                }
            }

            // Assign Admin role
            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                var addRole = await userManager.AddToRoleAsync(adminUser, "Admin");

                if(!addRole.Succeeded)
                {
                    var errors = string.Join("; ", addRole.Errors.Select(e => $"{e.Code}:{e.Description}"));
                    throw new Exception($"Failed to add seed admin to role: {errors}");
                }

            }
        }
    }
}
