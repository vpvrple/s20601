using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class MovieOfTheDay
{
    public string Movie_Id { get; set; } = null!;

    public DateTime Date { get; set; }

    public virtual Movie Movie { get; set; } = null!;
}
