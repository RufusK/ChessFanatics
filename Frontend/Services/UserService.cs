using System.Security.Authentication;
using Core.Models;
using Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Frontend.Services;

public class UserService(
    AuthenticationStateProvider authenticationStateProvider,
    IDbContextFactory<AppDbContext> dbContextFactory)
{
    public async Task<User> GetUser()
    {
        var authState = await authenticationStateProvider.GetAuthenticationStateAsync();

        if (authState.User.Identity?.IsAuthenticated != true)
        {
            throw new AuthenticationException("No user authenticated");
        }

        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == authState.User.Identity.Name!);
        if (user is not null)
        {
            return user;
        }

        throw new AuthenticationException("No user authenticated");
    }
}