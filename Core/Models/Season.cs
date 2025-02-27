using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Season : BaseEntity
{
    [Required] public required string Name { get; set; }
    public Guid LeagueId { get; set; }
    public League League { get; set; } = null!;
}