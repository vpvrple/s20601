using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class Post
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int IdGroup { get; set; }

    public int IdUser { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? LastModifiedAt { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Group IdGroupNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
