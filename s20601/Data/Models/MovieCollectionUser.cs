using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class MovieCollectionUser
{
    public int Id { get; set; }

    public int IdUser { get; set; }

    public int IdMovieCollection { get; set; }

    public int IdRole { get; set; }

    public virtual MovieCollection IdMovieCollectionNavigation { get; set; } = null!;

    public virtual Role IdRoleNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
