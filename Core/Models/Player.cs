namespace Core.Models;

public class Player : BaseEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public ICollection<LeagueParticipation> LeagueParticipations { get; } = [];

    public ICollection<SeasonParticipation> SeasonParticipations { get; } = [];
    public ICollection<TournamentParticipation> TournamentParticipations { get; } = [];
}