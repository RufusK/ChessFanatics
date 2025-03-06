using Core.Models;
using Infrastructure;

namespace Frontend.Services;

public class LeagueService(UserService userService, AppDbContext dbContext)
{
    public async Task<League> GetLeague(string leagueId)
    {
        var user = await userService.GetUser();
        var league = await dbContext.Leagues.FindAsync(int.Parse(leagueId));
        if (league is not null && league.Organizers.Any(x => x.UserId == user.Id))
        {
            return league;
        }

        throw new KeyNotFoundException("The League was not found.");
    }
}