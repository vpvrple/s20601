using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class GroupMembership
{
    public int IdUser { get; set; }

    public int IdGroup { get; set; }

    public DateTime JoinedAt { get; set; }

    public virtual Group IdGroupNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
