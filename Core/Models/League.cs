using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class League : BaseEntity
{
    [Required] public required string Name { get; set; }

    public ICollection<LeagueOrganizer> Organizers { get; } = new List<LeagueOrganizer>();
    public ICollection<Season> Seasons { get; } = new List<Season>();
    public ICollection<LeagueParticipation> LeagueParticipations { get; } = new List<LeagueParticipation>();
}