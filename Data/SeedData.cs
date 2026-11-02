using EmployeeLeaveManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveManagementSystem.Data;
public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider services, IConfiguration config)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        // Apply migrations automatically when migrations exist. Create the initial migration with the README commands.
        if (db.Database.GetMigrations().Any()) await db.Database.MigrateAsync();
        var roles = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        foreach (var role in new[] { "Admin", "Employee" })
            if (!await roles.RoleExistsAsync(role)) await roles.CreateAsync(new IdentityRole(role));
        var email = Environment.GetEnvironmentVariable("ELMS_ADMIN_EMAIL")
            ?? config["SeedAdmin:Email"];

        var password = Environment.GetEnvironmentVariable("ELMS_ADMIN_PASSWORD")
               ?? config["SeedAdmin:Password"];
        if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
        {
            var users = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var admin = await users.FindByEmailAsync(email);
            if (admin is null)
            {
                admin = new ApplicationUser { UserName = email, Email = email, EmailConfirmed = true, FullName = "System Administrator" };
                var result = await users.CreateAsync(admin, password);
                if (result.Succeeded) await users.AddToRoleAsync(admin, "Admin");
            }
            else if (!await users.IsInRoleAsync(admin, "Admin")) await users.AddToRoleAsync(admin, "Admin");
        }
    }
}
