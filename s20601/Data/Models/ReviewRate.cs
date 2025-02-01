using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class ReviewRate
{
    public int IdUser { get; set; }

    public bool Rating { get; set; }

    public DateTime RatedAt { get; set; }

    public int Review_IdAuthor { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual Review Review_IdAuthorNavigation { get; set; } = null!;
}
