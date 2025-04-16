namespace s20601.Data.Models;

public partial class Post
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int IdGroup { get; set; }

    public string IdUser { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int? LastModifiedAt { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = [];

    public virtual Group IdGroupNavigation { get; set; } = null!;

    public virtual ApplicationUser IdUserNavigation { get; set; } = null!;
}
