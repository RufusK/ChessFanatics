namespace Api.Models;

public class TournamentRound(IEnumerable<Game> games)
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public List<Game> Games { get; init; } = games.ToList();
}