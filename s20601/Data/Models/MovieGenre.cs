using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class MovieGenre
{
    public int IdGenre { get; set; }

    public string Movie_Id { get; set; } = null!;

    public virtual Genre IdGenreNavigation { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
