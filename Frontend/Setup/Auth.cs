using System.Security.Claims;
using Core.Models;
using Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Frontend.Setup;

public static class Auth
{
    public static void AddAuth(this IServiceCollection services)
    {
        services.AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddIdentityCookies();

        services.AddAuthorization();

        services.AddIdentityCore<User>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireLowercase = true;
                opt.ClaimsIdentity.UserIdClaimType = ClaimTypes.PrimarySid;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddSignInManager()
            .AddApiEndpoints();
    }
}