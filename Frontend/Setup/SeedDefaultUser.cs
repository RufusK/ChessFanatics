using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Frontend.Setup;

public static class WebApplicationExtensions
{
    public static async Task EnsureDefaultUserExists(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<IdentityUser>>();

        // Read email and password from the ConnectionStrings section
        var email = configuration.GetConnectionString("DefaultUserEmail");
        var password = configuration.GetConnectionString("DefaultUserPassword");

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            logger.LogWarning("Default user email or password is not configured in ConnectionStrings.");
            return;
        }

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
        {
            user = new User { UserName = email, Email = email };
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                logger.LogInformation("User {Email} created successfully.", email);
            }
            else
            {
                logger.LogError("Error creating user {Email}: {Errors}", email, string.Join(", ", result.Errors));
            }
        }
        else
        {
            logger.LogInformation("User {Email} already exists.", email);
        }
    }
}