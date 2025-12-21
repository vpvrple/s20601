using s20601.Data;
using s20601.Data.Models;

namespace s20601.Services;

using Microsoft.EntityFrameworkCore;

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

        var friends = await context.UserRelationships
            .Where(x => x.IdUser == userId || x.IdRelatedUser == userId && x.Type == RelationshipType.Pending)
            .Select(x => x.IdUser == userId ? x.IdRelatedUserNavigation : x.IdUserNavigation)
            .ToListAsync();

        return friends;
    }
    
    public async Task<List<ApplicationUser>> GetFriends(string userId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var friends = await context.UserRelationships
            .Where(x => x.IdUser == userId || x.IdRelatedUser == userId && x.Type == RelationshipType.Friends)
            .Select(x => x.IdUser == userId ? x.IdRelatedUserNavigation : x.IdUserNavigation)
            .ToListAsync();

        return friends;
    }
    

    public async Task<bool> AreFriends(string userId, string friendId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.UserRelationships.AnyAsync(ur =>
            ((ur.IdUser == userId && ur.IdRelatedUser == friendId) ||
            (ur.IdUser == friendId && ur.IdRelatedUser == userId)) && ur.Type == RelationshipType.Friends);
    }

    public async Task SendFriendRequest(string requesterId, string receiverId)
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
            Type = RelationshipType.Pending
        };

        context.UserRelationships.Add(newRequest);
        await context.SaveChangesAsync();
    }

    public async Task AcceptFriendRequest(string approverId, string requesterId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        
        var request = await context.UserRelationships.FirstOrDefaultAsync(ur =>
            ur.IdUser == requesterId && ur.IdRelatedUser == approverId && ur.Type == RelationshipType.Pending);

        if (request != null)
        {
            request.Type = RelationshipType.Friends;
            await context.SaveChangesAsync();
        }
    }
    
    public async Task RemoveFriend(string userId, string friendId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        
        var relationship = await context.UserRelationships.FirstOrDefaultAsync(ur =>
            ((ur.IdUser == userId && ur.IdRelatedUser == friendId) ||
             (ur.IdUser == friendId && ur.IdRelatedUser == userId)) && ur.Type == RelationshipType.Friends);

        if (relationship != null)
        {
            context.UserRelationships.Remove(relationship);
            await context.SaveChangesAsync();
        }
    }

    public async Task DeclineFriendRequest(string userId, string requesterId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        
        var request = await context.UserRelationships.FirstOrDefaultAsync(ur =>
            ur.IdUser == requesterId && ur.IdRelatedUser == userId && ur.Type == RelationshipType.Pending);

        if (request != null)
        {
            context.UserRelationships.Remove(request);
            await context.SaveChangesAsync();
        }
    }
}