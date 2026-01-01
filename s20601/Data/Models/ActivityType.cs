namespace s20601.Data.Models;

public partial class ActivityType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<SocialActivityLog> SocialActivityLogs { get; set; } = new List<SocialActivityLog>();
}
