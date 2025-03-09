namespace Core.Models;

public class Game: BaseEntity
{
    public int? PlayerWhiteId { get; set; }
    public int? PlayerBlackId { get; set; }
    public GameResult Result { get; set; } = GameResult.None;
    
    public int TournamentRoundId { get; set; }
    public TournamentRound TournamentRound { get; set; }
}