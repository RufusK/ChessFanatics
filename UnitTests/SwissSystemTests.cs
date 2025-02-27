using Core.Models;

namespace UnitTests;

[TestFixture]
public class SwissSystemTests
{
    private List<Player> _players;

    [SetUp]
    public void SetUp()
    {
        // Adjusting Player creation to include both Name and Email
        _players =
        [
            new Player("Alice", "Mayer", "alice@example.com"),
            new Player("Bob", "Mayer", "bob@example.com"),
            new Player("Charlie", "Mayer", "charlie@example.com"),
            new Player("David", "Mayer", "david@example.com"),
            new Player("Eve", "Maier", "eve@example.com")
        ];
    }

    [Test]
    public void GenerateNextRound_ShouldCreateCorrectNumberOfGames()
    {
        var previousRounds = new List<TournamentRound>();
        var games = SwissSystem.GenerateNextRound(_players, previousRounds);

        Assert.That(games.Count, Is.EqualTo(_players.Count / 2 + _players.Count % 2));
    }

    [Test]
    public void GenerateNextRound_ShouldAlternateColors()
    {
        var previousRounds = new List<TournamentRound>
        {
            new
            (
                new List<Game>
                {
                    new Game(_players[0], _players[1]) { Result = GameResult.WhiteWin }, // Alice won as White
                    new Game(_players[2], _players[3]) { Result = GameResult.Draw } // Charlie and David drew
                }
            )
        };

        var games = SwissSystem.GenerateNextRound(_players, previousRounds);

        // Count how often each player plays as White
        var whiteCounts = games.GroupBy(g => g.White).ToDictionary(g => g.Key, g => g.Count());

        Assert.That(whiteCounts.Values.All(count => count <= 2), Is.True); // No player should play White too often
    }

    [Test]
    public void GenerateNextRound_ShouldNotRepeatOpponents()
    {
        var previousRounds = new List<TournamentRound>
        {
            new
            (new List<Game>
                {
                    new Game(_players[0], _players[1]) { Result = GameResult.WhiteWin }, // Alice vs Bob
                    new Game(_players[2], _players[3]) { Result = GameResult.Draw } // Charlie vs David
                }
            )
        };

        var games = SwissSystem.GenerateNextRound(_players, previousRounds);

        foreach (var game in games)
        {
            if (game.Black != null)
            {
                var previousMatchups = previousRounds.SelectMany(r => r.Games)
                    .Where(g => g.Black != null)
                    .Select(g => new HashSet<Player> { g.White, g.Black });
                Assert.That(previousMatchups.Any(set => set.SetEquals(new HashSet<Player> { game.White, game.Black })),
                    Is.False);
            }
        }
    }

    [Test]
    public void GenerateNextRound_ShouldHandleByeCorrectly()
    {
        var previousRounds = new List<TournamentRound>();
        var games = SwissSystem.GenerateNextRound(_players, previousRounds);

        var byeGames = games.Where(g => g.Result == GameResult.Bye).ToList();

        Assert.That(byeGames.Count, Is.EqualTo(1)); // There should be exactly one bye
        Assert.That(byeGames[0].Black, Is.Null); // Bye game should have no Black player
    }

    [Test]
    public void GenerateNextRound_ShouldAwardByePoint()
    {
        var previousRounds = new List<TournamentRound>();
        var games = SwissSystem.GenerateNextRound(_players, previousRounds);

        var byePlayer = games.FirstOrDefault(g => g.Result == GameResult.Bye)?.White;

        var stats = SwissSystem.GenerateNextRound(_players, new List<TournamentRound>
        {
            new TournamentRound(games)
        });

        var newStats = stats.Where(g => Equals(g.White, byePlayer) || Equals(g.Black, byePlayer));

        Assert.That(newStats.Any(g => Equals(g.White, byePlayer) || Equals(g.Black, byePlayer)), Is.True);
    }
}