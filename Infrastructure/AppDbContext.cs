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
    public DbSet<Game> Games { get; set; }
    
    

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("identity");
        
        builder.Entity<Game>()
            .HasOne(g => g.PlayerWhite)
            .WithMany(p => p.GamesAsWhite)
            .HasForeignKey(g => g.PlayerWhiteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Game>()
            .HasOne(g => g.PlayerBlack)
            .WithMany(p => p.GamesAsBlack)
            .HasForeignKey(g => g.PlayerBlackId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}