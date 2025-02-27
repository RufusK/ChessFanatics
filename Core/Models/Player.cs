using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Player(string firstName, string lastName, string email) : BaseEntity
{
    [Required] public string FirstName { get; set; } = firstName;

    [Required] public string LastName { get; set; } = lastName;

    [Required] public string Email { get; set; } = email;

    public ICollection<LeagueParticipation> LeagueParticipations { get; } = new List<LeagueParticipation>();
}