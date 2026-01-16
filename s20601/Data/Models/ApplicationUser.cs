using Microsoft.AspNetCore.Identity;

namespace s20601.Data.Models;

public class ApplicationUser : IdentityUser
{
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLogin { get; set; }
    public DateTime? LastDailyLogin { get; set; }
    public string? ProfileDescription { get; set; }
    public int ReputationPoints { get; set; }
    public string? Avatar { get; set; }
    public virtual ICollection<Message> MessageIdRecipientNavigations { get; set; } = new List<Message>();
    public virtual ICollection<Message> MessageIdSenderNavigations { get; set; } = new List<Message>();
    public virtual ICollection<MovieCollectionUser> MovieCollectionUsers { get; set; } = new List<MovieCollectionUser>();
    public virtual ICollection<MovieRate> MovieRates { get; set; } = new List<MovieRate>();
    public virtual ICollection<MovieUpdateRequest> MovieUpdateRequests { get; set; } = new List<MovieUpdateRequest>();
    public virtual ICollection<ReviewRate> ReviewRates { get; set; } = new List<ReviewRate>();
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    public virtual ICollection<UserRelationship> UserRelationshipIdRelatedUserNavigations { get; set; } = new List<UserRelationship>();
    public virtual ICollection<UserRelationship> UserRelationshipIdUserNavigations { get; set; } = new List<UserRelationship>();
}
