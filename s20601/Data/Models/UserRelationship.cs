using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class UserRelationship
{
    public int IdUser { get; set; }

    public int IdRelatedUser { get; set; }

    public string Type { get; set; } = null!;

    public virtual User IdRelatedUserNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
