using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class MovieCollectionMovie
{
    public int IdMovieCollection { get; set; }

    public DateTime AddedAt { get; set; }

    public string Movie_Id { get; set; } = null!;

    public virtual MovieCollection IdMovieCollectionNavigation { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
