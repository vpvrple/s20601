using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class MovieCollection
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual MovieCollectionMovie? MovieCollectionMovie { get; set; }

    public virtual ICollection<MovieCollectionUser> MovieCollectionUsers { get; set; } = new List<MovieCollectionUser>();

    public override string ToString()
    {
        return Name;
    }
}
