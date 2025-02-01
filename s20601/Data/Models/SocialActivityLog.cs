using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class SocialActivityLog
{
    public int Id { get; set; }

    public int IdUser { get; set; }

    public int IdActivityType { get; set; }

    public DateTime ActivityAt { get; set; }

    public virtual ActivityType IdActivityTypeNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
