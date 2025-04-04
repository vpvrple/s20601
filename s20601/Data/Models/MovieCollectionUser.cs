using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class MovieCollectionUser
{
    public int Id { get; set; }

    public string IdUser { get; set; } = null!;

    public int IdMovieCollection { get; set; }

    public virtual MovieCollection IdMovieCollectionNavigation { get; set; } = null!;

    public virtual ApplicationUser IdUserNavigation { get; set; } = null!;
}
