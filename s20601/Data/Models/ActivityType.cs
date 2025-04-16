namespace s20601.Data.Models;

public partial class ActivityType
{
    public int Id { get; set; }

    public int Name { get; set; }

    public virtual ICollection<SocialActivityLog> SocialActivityLogs { get; set; } = [];
}
