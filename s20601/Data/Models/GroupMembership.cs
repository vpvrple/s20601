using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class GroupMembership
{
    public string IdUser { get; set; } = null!;

    public int IdGroup { get; set; }

    public DateTime JoinedAt { get; set; }

    public virtual Group IdGroupNavigation { get; set; } = null!;

    public virtual ApplicationUser IdUserNavigation { get; set; } = null!;
}
