namespace Core.Models;

public class TournamentRound: BaseEntity
{
    public int RoundNumber { get; set; }
    public ICollection<Game> Games { get; set; } = [];
    
    public int TournamentId { get; set; }
    
    public Tournament Tournament { get; set; }
}