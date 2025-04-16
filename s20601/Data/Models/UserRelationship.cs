namespace s20601.Data.Models;

public partial class UserRelationship
{
    public string IdUser { get; set; } = null!;

    public string IdRelatedUser { get; set; } = null!;

    public string Type { get; set; } = null!;

    public virtual ApplicationUser IdRelatedUserNavigation { get; set; } = null!;

    public virtual ApplicationUser IdUserNavigation { get; set; } = null!;
}
