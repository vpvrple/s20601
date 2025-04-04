using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class Status
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; } = [];
}
