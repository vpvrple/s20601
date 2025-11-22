namespace s20601.Data.Models;

public partial class Group
{
    public int Id { get; set; }

    public int Name { get; set; }

    public string? Description { get; set; }

    public byte[] Image { get; set; } = null!;

    public virtual ICollection<GroupMembership> GroupMemberships { get; set; } = new List<GroupMembership>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<ApplicationUser> IdOwners { get; set; } = new List<ApplicationUser>();
}
