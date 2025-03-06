namespace Core.Models;

public class Tournament: BaseEntity
{
    public required string Name { get; set; }

    public required int NumberOfRounds { get; set; }

    public int CurrentRound { get; private set; } = 0;

    public ICollection<TournamentRound> Rounds { get; init; } = [];

    public ICollection<Player> Players { get; init; } = [];

    public void AddPlayer(Player player)
    {
        Players.Add(player);
    }

    public void CalculateNextRound()
    {
        if (CurrentRound >= NumberOfRounds)
        {
            throw new DomainException("The tournament is finished.");
        }

        if (CurrentRound == 0)
        {
            CalculateFirstRound();
        }
    }

    private static Random rng = new Random();

    private void CalculateFirstRound()
    {
        var shuffledPlayers = Players.OrderBy(_ => rng.Next());
        var games = PlayersToGames(shuffledPlayers);
        Rounds.Add(new TournamentRound
        {
            Games = games.ToList()
        });
    }

    private static IEnumerable<Game> PlayersToGames(IOrderedEnumerable<Player> players)
    {
       var games = players.Chunk(2).ToList();
        
        foreach (var game in games)
        {
            if (game.Length > 1)
            {
                yield return new Game(game[0], game[1]);
                continue;
            }

            var whiteOrBlack = rng.Next(0, 2);
            if (whiteOrBlack == 0)
            {
                yield return new Game(game[0]);
                continue;
            }

            yield return new Game(null, game[0]);
        }
    }

}