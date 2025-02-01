using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class Genre
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual MovieGenre? MovieGenre { get; set; }
}
