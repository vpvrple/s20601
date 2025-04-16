namespace s20601.Data.Models;

public partial class MovieCrew
{
    public int Id { get; set; }

    public int IdCrew { get; set; }

    public string Job { get; set; } = null!;

    public string? CharacterName { get; set; }

    public int Movie_Id { get; set; }

    public virtual Crew IdCrewNavigation { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
