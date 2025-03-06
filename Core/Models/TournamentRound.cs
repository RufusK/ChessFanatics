namespace Core.Models;

public class TournamentRound: BaseEntity
{
    public ICollection<Game> Games { get; set; } = [];
}