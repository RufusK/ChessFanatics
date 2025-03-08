using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<League> Leagues { get; set; }
    public DbSet<LeagueOrganizer> LeagueOrganizers { get; set; }
    public DbSet<LeagueParticipation> LeagueParticipations { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }
    public DbSet<TournamentRound> TournamentRounds { get; set; }
    public DbSet<TournamentParticipation> TournamentParticipations { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("identity");
    }
}