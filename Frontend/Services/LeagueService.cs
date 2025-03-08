using Core.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Frontend.Services;

public class LeagueService(UserService userService, IDbContextFactory<AppDbContext> dbContextFactory)
{
    public async Task<League> GetLeague(string leagueId)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        var user = await userService.GetUser();
        var league = await dbContext.Leagues
            .Include(x => x.Organizers)
            .FirstOrDefaultAsync(x => x.Id == int.Parse(leagueId));
        if (league is not null && league.Organizers.Any(x => x.UserId == user.Id))
        {
            return league;
        }

        throw new KeyNotFoundException("The League was not found.");
    }
}