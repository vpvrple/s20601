using s20601.Data;
using s20601.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace s20601.Services;

public class FriendService : IFriendService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    public FriendService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    
    public async Task<List<ApplicationUser>> GetFriendRequests(string userId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        // Return users who sent a request TO the current user (userId)
        return await context.UserRelationships
            .Include(x => x.IdUserNavigation)
            .Where(x => x.IdRelatedUser == userId && x.Type == RelationshipType.Pending)
            .Select(x => x.IdUserNavigation)
            .ToListAsync();
    }
    
    public async Task<List<ApplicationUser>> GetFriends(string userId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var friendsAsUser = await context.UserRelationships
            .Include(x => x.IdRelatedUserNavigation)
            .Where(x => x.IdUser == userId && x.Type == RelationshipType.Friends)
            .Select(x => x.IdRelatedUserNavigation)
            .ToListAsync();

        var friendsAsRelated = await context.UserRelationships
            .Include(x => x.IdUserNavigation)
            .Where(x => x.IdRelatedUser == userId && x.Type == RelationshipType.Friends)
            .Select(x => x.IdUserNavigation)
            .ToListAsync();

        return friendsAsUser.Concat(friendsAsRelated).ToList();
    }

    public async Task<RelationshipType?> GetUsersRelationshipType(string userId, string friendId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var relationship = await context.UserRelationships.FirstOrDefaultAsync(ur =>
            (ur.IdUser == userId && ur.IdRelatedUser == friendId) ||
            (ur.IdUser == friendId && ur.IdRelatedUser == userId));

        return relationship?.Type;
    }

    public async Task SendFriendRequest(string requesterId, string receiverId, string message)
    {
        if (requesterId == receiverId)
        {
            return;
        }

        using var context = await _dbContextFactory.CreateDbContextAsync();
        
        var relationshipExists = await context.UserRelationships.AnyAsync(ur =>
            (ur.IdUser == requesterId && ur.IdRelatedUser == receiverId) ||
            (ur.IdUser == receiverId && ur.IdRelatedUser == requesterId));

        if (relationshipExists)
        {
            return;
        }

        var newRequest = new UserRelationship
        {
            IdUser = requesterId,
            IdRelatedUser = receiverId,
            Message = message,
            Type = RelationshipType.Pending
        };

        context.UserRelationships.Add(newRequest);
        await context.SaveChangesAsync();
    }

    public async Task AcceptFriendRequest(string approverId, string requesterId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        
        // Find the request sent BY requesterId TO approverId
        await context.UserRelationships
            .Where(ur => ur.IdUser == requesterId && ur.IdRelatedUser == approverId && ur.Type == RelationshipType.Pending)
            .ExecuteUpdateAsync(setters => setters.SetProperty(s => s.Type, RelationshipType.Friends));
    }
    
    public async Task DeclineFriendRequest(string approverId, string requesterId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        
        // Find the request sent BY requesterId TO approverId
        await context.UserRelationships
            .Where(ur => ur.IdUser == requesterId && ur.IdRelatedUser == approverId && ur.Type == RelationshipType.Pending)
            .ExecuteDeleteAsync();
    }
    
    public async Task RemoveFriend(string userId, string friendId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        
        await context.UserRelationships
            .Where(ur => ((ur.IdUser == userId && ur.IdRelatedUser == friendId) ||
                          (ur.IdUser == friendId && ur.IdRelatedUser == userId)) && ur.Type == RelationshipType.Friends)
            .ExecuteDeleteAsync();
    }

    public async Task<string?> GetFriendRequestMessage(string requesterId, string receiverId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        
        var request = await context.UserRelationships
            .Where(ur => ur.IdUser == requesterId && ur.IdRelatedUser == receiverId && ur.Type == RelationshipType.Pending)
            .Select(ur => ur.Message)
            .FirstOrDefaultAsync();
            
        return request;
    }
}