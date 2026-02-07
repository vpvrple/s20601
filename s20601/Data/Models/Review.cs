namespace s20601.Data.Models;

public partial class Review
{
    public int Id { get; set; }

    public string IdAuthor { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? LastModifiedAt { get; set; }

    public int Movie_Id { get; set; }

    public virtual ApplicationUser IdAuthorNavigation { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;

    public virtual ICollection<ReviewRate> ReviewRates { get; set; } = new List<ReviewRate>();
}
