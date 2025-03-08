namespace Core.Models;

public class Game: BaseEntity
{
    public Game(int playerWhiteId, int? playerBlackId = null)
    {
        PlayerWhiteId = playerWhiteId;
        PlayerBlackId = playerBlackId;
        if (playerBlackId is null)
        {
            Result = GameResult.Bye;
        }
    }
    public int PlayerWhiteId { get; set; }
    public Player PlayerWhite { get; set; }
    public int? PlayerBlackId { get; set; }
    public Player PlayerBlack { get; set; }
    public GameResult? Result { get; set; }
    
    public int TournamentRoundId { get; set; }
    public TournamentRound TournamentRound { get; set; }
}