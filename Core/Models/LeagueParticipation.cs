namespace Core.Models;

public class LeagueParticipation : BaseEntity
{
    public decimal Elo { get; set; } = 1500;

    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;

    public int LeagueId { get; set; }
    public League League { get; set; } = null!;
}