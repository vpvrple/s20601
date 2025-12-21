using s20601.Data.Models;

namespace s20601.Services;

public interface IFriendService
{
    Task<List<ApplicationUser>> GetFriends(string userId);
    Task<bool> AreFriends(string userId, string friendId);
    Task<List<ApplicationUser>> GetFriendRequests(string userId);
    Task AcceptFriendRequest(string approverId, string requesterId);
    Task RemoveFriend(string userId, string friendId);
    Task DeclineFriendRequest(string userId, string requesterId);
}