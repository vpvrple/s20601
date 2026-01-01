using s20601.Data.Models;

namespace s20601.Services;

public interface IFriendService
{
    Task<List<ApplicationUser>> GetFriends();
    Task<RelationshipType?> GetUsersRelationshipType(string userId2);
    Task<List<ApplicationUser>> GetFriendRequests();
    Task AcceptFriendRequest(string requesterId);
    Task RemoveFriend(string friendId);
    Task DeclineFriendRequest(string requesterId);
    Task SendFriendRequest(string receiverId, string message);
    Task<string?> GetFriendRequestMessage(string receiverId);
}