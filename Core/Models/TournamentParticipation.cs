namespace Core.Models;

public class TournamentParticipation : BaseEntity
{
    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;

    public int TournamentId { get; set; }
    public Tournament Tournament { get; set; } = null!;

    public bool IsActive = true;
}