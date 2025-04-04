using Microsoft.AspNetCore.Identity;
using s20601.Data.Models;

namespace s20601.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string? ProfileDescription { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ReputationPoints { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = [];

        public virtual ICollection<GroupMembership> GroupMemberships { get; set; } = [];

        public virtual ICollection<Message> MessageIdRecipientNavigations { get; set; } = [];

        public virtual ICollection<Message> MessageIdSenderNavigations { get; set; } = [];

        public virtual ICollection<MovieCollectionUser> MovieCollectionUsers { get; set; } = [];

        public virtual ICollection<MovieRate> MovieRates { get; set; } = [];

        public virtual ICollection<MovieUpdateRequest> MovieUpdateRequests { get; set; } = [];

        public virtual ICollection<Post> Posts { get; set; } = [];

        public virtual Review? Review { get; set; }

        public virtual ReviewRate? ReviewRate { get; set; }

        public virtual ICollection<SocialActivityLog> SocialActivityLogs { get; set; } = [];

        public virtual ICollection<UserRelationship> UserRelationshipIdRelatedUserNavigations { get; set; } = [];

        public virtual ICollection<UserRelationship> UserRelationshipIdUserNavigations { get; set; } = [];

        public virtual ICollection<Group> IdGroups { get; set; } = [];
    }

}
