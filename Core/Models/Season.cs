using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Season : BaseEntity
{
    [Required] public required string Name { get; set; }
    public int LeagueId { get; init; }
    public League League { get; init; } = null!;
}