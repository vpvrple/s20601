using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int ReputationPoints { get; set; }

    public DateTime? LastLogin { get; set; }

    public string? ProfileDescription { get; set; }

    public byte[]? ProfileImage { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<GroupMembership> GroupMemberships { get; set; } = new List<GroupMembership>();

    public virtual ICollection<Message> MessageIdRecipientNavigations { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageIdSenderNavigations { get; set; } = new List<Message>();

    public virtual ICollection<MovieCollectionUser> MovieCollectionUsers { get; set; } = new List<MovieCollectionUser>();

    public virtual ICollection<MovieRate> MovieRates { get; set; } = new List<MovieRate>();

    public virtual ICollection<MovieUpdateRequest> MovieUpdateRequests { get; set; } = new List<MovieUpdateRequest>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual Review? Review { get; set; }

    public virtual ReviewRate? ReviewRate { get; set; }

    public virtual ICollection<SocialActivityLog> SocialActivityLogs { get; set; } = new List<SocialActivityLog>();

    public virtual ICollection<UserRelationship> UserRelationshipIdRelatedUserNavigations { get; set; } = new List<UserRelationship>();

    public virtual ICollection<UserRelationship> UserRelationshipIdUserNavigations { get; set; } = new List<UserRelationship>();

    public virtual ICollection<Group> IdGroups { get; set; } = new List<Group>();
}
