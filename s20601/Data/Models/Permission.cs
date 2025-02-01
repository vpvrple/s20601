using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class Permission
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
