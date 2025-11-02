using Domain.Entities.Schema.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace HeadFirstOOAD.Helpers
{
    public static class DbInitializer
    {
        /*
        PSEUDOCODE / PLAN:
        1. Create a scoped service provider from the incoming IServiceProvider.
        2. Resolve RoleManager<IdentityRole> and UserManager<IdentityUser> from the scope.
        3. Define an array of role names to ensure exist in the database (e.g., "Admin", "Member").
        4. For each role:
           - Check if the role exists via RoleManager.RoleExistsAsync.
           - If it does not exist, create it via RoleManager.CreateAsync.
        5. Define default users to seed:
           - Admin user: email, username, password, assigned to "Admin" role.
           - Member user: email, username, password, assigned to "Member" role.
        6. For each default user:
           - Check if user exists by email using UserManager.FindByEmailAsync.
           - If not found, create a new IdentityUser with EmailConfirmed = true and call UserManager.CreateAsync with password.
           - If creation succeeded (or user already exists), ensure the user is in the expected role using UserManager.AddToRoleAsync when not already in role.
        7. Handle failures by throwing an exception with details (so failures are visible in startup/logs) or ignore depending on policy.
        */

        public async static Task seedRolesAndUser(this IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));

            using var scope = serviceProvider.CreateScope();
            var scopedProvider = scope.ServiceProvider;

            var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scopedProvider.GetRequiredService<UserManager<Users>>();

            // Roles to ensure exist
            var roles = new[] { "Admin", "Member" };

            foreach (var roleName in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    var roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                    if (!roleResult.Succeeded)
                    {
                        throw new InvalidOperationException($"Failed to create role '{roleName}': {string.Join(", ", roleResult.Errors)}");
                    }
                }
            }

            // Default users to seed
            var adminEmail = "admin@localhost";
            var adminUserName = "admin";
            var adminPassword = "Admin@123"; // Replace with secure secret in production

            var memberEmail = "member@localhost";
            var memberUserName = "member";
            var memberPassword = "Member@123"; // Replace with secure secret in production

            // Seed Admin user
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new Users
                {
                    UserName = adminUserName,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var createAdminResult = await userManager.CreateAsync(adminUser, adminPassword);
                if (!createAdminResult.Succeeded)
                {
                    throw new InvalidOperationException($"Failed to create admin user '{adminEmail}': {string.Join(", ", createAdminResult.Errors)}");
                }
            }

            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                var addToRoleResult = await userManager.AddToRoleAsync(adminUser, "Admin");
                if (!addToRoleResult.Succeeded)
                {
                    throw new InvalidOperationException($"Failed to add user '{adminEmail}' to role 'Admin': {string.Join(", ", addToRoleResult.Errors)}");
                }
            }

            // Seed Member user
            var memberUser = await userManager.FindByEmailAsync(memberEmail);
            if (memberUser == null)
            {
                memberUser = new Users
                {
                    UserName = memberUserName,
                    Email = memberEmail,
                    EmailConfirmed = true
                };

                var createMemberResult = await userManager.CreateAsync(memberUser, memberPassword);
                if (!createMemberResult.Succeeded)
                {
                    throw new InvalidOperationException($"Failed to create member user '{memberEmail}': {string.Join(", ", createMemberResult.Errors)}");
                }
            }

            if (!await userManager.IsInRoleAsync(memberUser, "Member"))
            {
                var addToRoleResult = await userManager.AddToRoleAsync(memberUser, "Member");
                if (!addToRoleResult.Succeeded)
                {
                    throw new InvalidOperationException($"Failed to add user '{memberEmail}' to role 'Member': {string.Join(", ", addToRoleResult.Errors)}");
                }
            }
        }
    }
}
