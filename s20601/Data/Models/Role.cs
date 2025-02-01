using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MovieCollectionUser> MovieCollectionUsers { get; set; } = new List<MovieCollectionUser>();

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
