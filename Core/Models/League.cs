using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class League : BaseEntity
{
    [Required] public required string Name { get; set; }

    public ICollection<LeagueOrganizer> Organizers { get; } = [];
    public ICollection<Season> Seasons { get; } = [];
    public ICollection<LeagueParticipation> LeagueParticipations { get; } = [];
}