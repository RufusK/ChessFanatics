namespace Core.Models;

public class Tournament : BaseEntity
{
    public required string Name { get; set; }

    public bool IsEloRated { get; set; } = true;

    public required int NumberOfRounds { get; set; }

    public int? SeasonId { get; set; }

    public Season? Season { get; set; }

    public int LeagueId { get; set; }

    public League League { get; set; }
    public int CurrentRound { get; private set; } = 0;

    public PairingSystem PairingSystem { get; set; } = PairingSystem.Manual;

    public ICollection<TournamentRound> Rounds { get; } = [];

    public ICollection<TournamentParticipation> TournamentParticipations { get; } = [];

    // public void CalculateNextRound()
    // {
    //     if (CurrentRound >= NumberOfRounds)
    //     {
    //         throw new DomainException("The tournament is finished.");
    //     }
    //
    //     if (CurrentRound == 0)
    //     {
    //         CalculateFirstRound();
    //     }
    // }

    private static Random rng = new Random();

    // private void CalculateFirstRound()
    // {
    //     var shuffledPlayers = TournamentParticipations
    //         .Where(x => x.IsActive)
    //         .Select(x => x.Player)
    //         .OrderBy(_ => rng.Next());
    //     var games = PlayersToGames(shuffledPlayers);
    //     Rounds.Add(new TournamentRound
    //     {
    //         Games = games.ToList()
    //     });
    // }

    // private static IEnumerable<Game> PlayersToGames(IOrderedEnumerable<Player> players)
    // {
    //     var games = players.Chunk(2).ToList();
    //
    //     foreach (var gamePlayers in games)
    //     {
    //         if (gamePlayers.Length > 1)
    //         {
    //             yield return new Game(gamePlayers[0].Id, gamePlayers[1].Id);
    //             continue;
    //         }
    //
    //         // BYE
    //         yield return new Game(gamePlayers[0].Id);
    //     }
    // }
}