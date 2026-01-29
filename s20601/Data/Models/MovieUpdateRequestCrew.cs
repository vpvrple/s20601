namespace s20601.Data.Models;

public class MovieUpdateRequestCrew
{
    public int Id { get; set; }
    public int MovieUpdateRequestId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int? BirthYear { get; set; }
    public int? DeathYear { get; set; }
    public string? Job { get; set; } = null!;
    public string? CharacterName { get; set; }
    public int? CrewId { get; set; }
    
    public virtual MovieUpdateRequest MovieUpdateRequest { get; set; } = null!;
}