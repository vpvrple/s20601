using s20601.Data.Interfaces;

namespace s20601.Data.Models;

public partial class Group : INavigable
{
    public int Id { get; set; }

    public int Name { get; set; }

    public string? Description { get; set; }

    public byte[] Image { get; set; } = null!;

    public virtual ICollection<GroupMembership> GroupMemberships { get; set; } = [];

    public virtual ICollection<Post> Posts { get; set; } = [];

    public virtual ICollection<ApplicationUser> IdOwners { get; set; } = [];

    public string GetUrl()
    {
        return $"group/{Id}";
    }
}
