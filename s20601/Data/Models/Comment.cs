using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class Comment
{
    public int Id { get; set; }

    public int IdPost { get; set; }

    public string IdUser { get; set; } = null!;

    public int? IdComment { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastModifiedAt { get; set; }

    public string Content { get; set; } = null!;

    public virtual Comment? IdCommentNavigation { get; set; }

    public virtual Post IdPostNavigation { get; set; } = null!;

    public virtual ApplicationUser IdUserNavigation { get; set; } = null!;

    public virtual ICollection<Comment> InverseIdCommentNavigation { get; set; } = [];
}
