using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class MovieCrew
{
    public int Id { get; set; }

    public int IdCrew { get; set; }

    public string? Job { get; set; }

    public string? CharacterName { get; set; }

    public string Movie_Id { get; set; } = null!;

    public virtual Crew IdCrewNavigation { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
