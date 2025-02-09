namespace Core.Models;

public class Game
{
    public Game(Player white, Player? black = null)
    {
        White = white;
        Black = black;
        if (black is null)
        {
            Result = GameResult.Bye;
        }
    }

    public Guid Id { get; init; } = Guid.NewGuid();

    public Player White { get; init; }

    public Player? Black { get; init; }

    public GameResult? Result { get; set; }
}