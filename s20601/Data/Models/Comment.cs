using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class Comment
{
    public int Id { get; set; }

    public int IdPost { get; set; }

    public int IdUser { get; set; }

    public int? IdComment { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastModifiedAt { get; set; }

    public string Content { get; set; } = null!;

    public virtual Comment? IdCommentNavigation { get; set; }

    public virtual Post IdPostNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual ICollection<Comment> InverseIdCommentNavigation { get; set; } = new List<Comment>();
}
