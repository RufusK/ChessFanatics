namespace Core.Models;

public class SeasonParticipation : BaseEntity
{
    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;

    public int SeasonId { get; set; }
    public Season Season { get; set; } = null!;
}