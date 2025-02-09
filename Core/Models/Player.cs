namespace Core.Models;

public class Player(string name, string email)
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; } = name;

    public string Email { get; set; } = email;
    
    public override bool Equals(object? obj) => obj is Player other && Name == other.Name;
    public override int GetHashCode() => Name.GetHashCode(); // Ensures correct dictionary behavior
}