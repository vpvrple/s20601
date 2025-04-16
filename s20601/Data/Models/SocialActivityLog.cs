namespace s20601.Data.Models;

public partial class SocialActivityLog
{
    public int Id { get; set; }

    public string IdUser { get; set; } = null!;

    public int IdActivityType { get; set; }

    public DateTime ActivityAt { get; set; }

    public virtual ActivityType IdActivityTypeNavigation { get; set; } = null!;

    public virtual ApplicationUser IdUserNavigation { get; set; } = null!;
}
