using System.Security.Authentication;
using Core.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Frontend.Services;

public class UserService(AuthenticationStateProvider authenticationStateProvider, UserManager<User> userManager)
{
    public async Task<User> GetUser()
    {
        var authState = await authenticationStateProvider.GetAuthenticationStateAsync();

        if (authState.User.Identity?.IsAuthenticated != true)
        {
            throw new AuthenticationException("No user authenticated");
        }

        var user = await userManager.FindByEmailAsync(authState.User.Identity?.Name!);
        if (user is not null)
        {
            return user;
        }

        throw new AuthenticationException("No user authenticated");
    }
}