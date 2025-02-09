namespace Core.Models;

public static class SwissSystem
{
    private class PlayerStats(Player player)
    {
        public Player Player { get; } = player;
        public int Score { get; set; }
        public int WhiteGames { get; set; }
        public int BlackGames { get; set; }
        public HashSet<Player> PreviousOpponents { get; } = new HashSet<Player>();

        public int ColorBalance => WhiteGames - BlackGames; // Used for pairing
    }

    public static List<Game> GenerateNextRound(List<Player> players, List<TournamentRound> previousRounds)
    {
        var stats = InitializePlayerStats(players, previousRounds);
        var sortedPlayers = SortPlayers(stats);
        var pairings = PairPlayers(sortedPlayers);
        HandleBye(sortedPlayers, pairings);

        return pairings;
    }

    private static Dictionary<Player, PlayerStats> InitializePlayerStats(List<Player> players, List<TournamentRound> rounds)
    {
        var stats = players.ToDictionary(p => p, p => new PlayerStats(p));

        foreach (var round in rounds)
        {
            foreach (var game in round.Games)
            {
                var whiteStats = stats[game.White];

                if (game.Black == null)
                {
                    whiteStats.Score += 1; // Bye point
                    continue;
                }

                var blackStats = stats[game.Black];

                // Update opponents list
                whiteStats.PreviousOpponents.Add(game.Black);
                blackStats.PreviousOpponents.Add(game.White);

                // Update colors played
                whiteStats.WhiteGames++;
                blackStats.BlackGames++;

                switch (game.Result)
                {
                    // Update scores
                    case GameResult.WhiteWin:
                        whiteStats.Score += 1;
                        break;
                    case GameResult.BlackWin:
                        blackStats.Score += 1;
                        break;
                    case GameResult.Draw:
                        whiteStats.Score += 0; // Change to 0.5 if draws should be counted
                        blackStats.Score += 0;
                        break;
                }
            }
        }

        return stats;
    }

    private static List<PlayerStats> SortPlayers(Dictionary<Player, PlayerStats> stats)
    {
        return stats.Values.OrderByDescending(p => p.Score)
            .ThenBy(p => p.ColorBalance)
            .ToList();
    }

    private static List<Game> PairPlayers(List<PlayerStats> sortedPlayers)
    {
        var pairings = new List<Game>();
        var paired = new HashSet<Player>();

        for (var i = 0; i < sortedPlayers.Count; i++)
        {
            if (paired.Contains(sortedPlayers[i].Player)) continue;

            for (int j = i + 1; j < sortedPlayers.Count; j++)
            {
                if (paired.Contains(sortedPlayers[j].Player) ||
                    sortedPlayers[i].PreviousOpponents.Contains(sortedPlayers[j].Player))
                    continue;

                // Assign colors based on balance
                bool p1PrefersWhite = sortedPlayers[i].WhiteGames <= sortedPlayers[i].BlackGames;
                bool p2PrefersWhite = sortedPlayers[j].WhiteGames <= sortedPlayers[j].BlackGames;

                Player white, black;
                if (p1PrefersWhite != p2PrefersWhite)
                {
                    white = p1PrefersWhite ? sortedPlayers[i].Player : sortedPlayers[j].Player;
                    black = p1PrefersWhite ? sortedPlayers[j].Player : sortedPlayers[i].Player;
                }
                else
                {
                    bool flip = new Random().Next(2) == 0;
                    white = flip ? sortedPlayers[i].Player : sortedPlayers[j].Player;
                    black = flip ? sortedPlayers[j].Player : sortedPlayers[i].Player;
                }

                pairings.Add(new Game(white, black));

                paired.Add(white);
                paired.Add(black);
                break;
            }
        }

        return pairings;
    }

    private static void HandleBye(List<PlayerStats> sortedPlayers, List<Game> pairings)
    {
        var pairedPlayers = pairings.SelectMany(g => new[] { g.White, g.Black }).Where(p => p != null).ToHashSet();
        var unpaired = sortedPlayers.Select(p => p.Player).FirstOrDefault(p => !pairedPlayers.Contains(p));

        if (unpaired != null)
        {
            pairings.Add(new Game(unpaired, null)); // Constructor handles the Bye result
        }
    }
}