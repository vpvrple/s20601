using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class MovieGenre
{
    public int Id { get; set; }

    public string Movie_Id { get; set; } = null!;

    public int Genre_Id { get; set; }

    public virtual Genre Genre { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
