using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class Review
{
    public int Id { get; set; }
    public int IdAuthor { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? LastModifiedAt { get; set; }

    public string Movie_Id { get; set; } = null!;

    public virtual User IdAuthorNavigation { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;

    public virtual ICollection<ReviewRate> ReviewRates { get; set; } = new List<ReviewRate>();
}
