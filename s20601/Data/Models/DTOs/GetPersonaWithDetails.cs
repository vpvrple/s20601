namespace s20601.Data.Models.DTOs;

public class GetPersonaWithDetails
{
    public int Id { get; set; }
    public string IMDBId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int BirthYear { get; set; }
    public int? DeathYear { get; set; }
    public ICollection<PersonaMovieRole> Movies { get; set; } = new List<PersonaMovieRole>();
}

public class PersonaMovieRole
{
    public int MovieId { get; set; }
    public string MovieUrl { get; set; } = null!;
    public string Title { get; set; } = null!;
    public int StartYear { get; set; }
    public string Job { get; set; } = null!;
    public string? CharacterName { get; set; }
}
