using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class MovieRate
{
    public int IdUser { get; set; }

    public byte Rating { get; set; }

    public DateTime RatedAt { get; set; }

    public string Movie_Id { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
