namespace Core.Models;

public class Player : BaseEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public decimal Elo { get; set; } = 1500;

    public int LeagueId { get; set; }
    public League League { get; set; }

    public ICollection<SeasonParticipation> SeasonParticipations { get; } = [];
    public ICollection<TournamentParticipation> TournamentParticipations { get; } = [];
}