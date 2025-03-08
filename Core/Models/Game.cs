namespace Core.Models;

public class Game: BaseEntity
{
    public Game(int whitePlayerId, int? blackPlayerId = null)
    {
        WhitePlayerId = whitePlayerId;
        BlackPlayerId = blackPlayerId;
        if (blackPlayerId is null)
        {
            Result = GameResult.Bye;
        }
    }
    public int WhitePlayerId { get; init; }
    public int? BlackPlayerId { get; init; }
    public GameResult? Result { get; set; }
}