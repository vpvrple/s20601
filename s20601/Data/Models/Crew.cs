namespace s20601.Data.Models;

public partial class Crew
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int BirthYear { get; set; }

    public int? DeathYear { get; set; }

    public virtual ICollection<MovieCrew> MovieCrews { get; set; } = [];
}
