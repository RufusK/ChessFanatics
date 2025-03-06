using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class LeagueOrganizer : BaseEntity
{
    [Required] public required string UserId { get; init; }

    public bool IsOwner { get; set; } = false;

    public int LeagueId { get; set; }
    public League League { get; set; } = null!;
}