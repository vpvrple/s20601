using s20601.Data;
using s20601.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace s20601.Services;

public class FriendService : IFriendService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IUserService _userService;
    public FriendService(IDbContextFactory<ApplicationDbContext> dbContextFactory, IUserService userService)
    {
        _dbContextFactory = dbContextFactory;
        _userService = userService;
    }
    
    public async Task<List<ApplicationUser>> GetFriendRequests()
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var authenticatedUserId = await _userService.GetAuthenticatedUserId();
        
        return await context.UserRelationships
            .Include(x => x.IdUserNavigation)
            .Where(x => x.IdRelatedUser == authenticatedUserId && x.Type == RelationshipType.Pending)
            .Select(x => x.IdUserNavigation)
            .ToListAsync();
    }
    
    public async Task<List<ApplicationUser>> GetFriends()
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var authenticatedUserId = await _userService.GetAuthenticatedUserId();
        
        var friendsAsUser = await context.UserRelationships
            .Include(x => x.IdRelatedUserNavigation)
            .Where(x => x.IdUser == authenticatedUserId && x.Type == RelationshipType.Friends)
            .Select(x => x.IdRelatedUserNavigation)
            .ToListAsync();

        var friendsAsRelated = await context.UserRelationships
            .Include(x => x.IdUserNavigation)
            .Where(x => x.IdRelatedUser == authenticatedUserId && x.Type == RelationshipType.Friends)
            .Select(x => x.IdUserNavigation)
            .ToListAsync();

        return friendsAsUser.Concat(friendsAsRelated).ToList();
    }

    public async Task<RelationshipType?> GetUsersRelationshipType(string friendId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var authenticatedUserId = await _userService.GetAuthenticatedUserId();
        
        var relationship = await context.UserRelationships.FirstOrDefaultAsync(ur =>
            (ur.IdUser == authenticatedUserId && ur.IdRelatedUser == friendId) ||
            (ur.IdUser == friendId && ur.IdRelatedUser == authenticatedUserId));

        return relationship?.Type;
    }

    public async Task SendFriendRequest(string receiverId, string message)
    {
        var authenticatedUserId = await _userService.GetAuthenticatedUserId();
        if (authenticatedUserId == receiverId)
        {
            return;
        }

        using var context = await _dbContextFactory.CreateDbContextAsync();
        
        var relationshipExists = await context.UserRelationships.AnyAsync(ur =>
            (ur.IdUser == authenticatedUserId && ur.IdRelatedUser == receiverId) ||
            (ur.IdUser == receiverId && ur.IdRelatedUser == authenticatedUserId));

        if (relationshipExists)
        {
            return;
        }

        var newRequest = new UserRelationship
        {
            IdUser = authenticatedUserId,
            IdRelatedUser = receiverId,
            Message = message,
            Type = RelationshipType.Pending
        };

        context.UserRelationships.Add(newRequest);
        await context.SaveChangesAsync();
    }

    public async Task AcceptFriendRequest(string requesterId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        
        var authenticatedUserId = await _userService.GetAuthenticatedUserId();
        
        await context.UserRelationships
            .Where(ur => ur.IdUser == requesterId && ur.IdRelatedUser == authenticatedUserId && ur.Type == RelationshipType.Pending)
            .ExecuteUpdateAsync(setters => setters.SetProperty(s => s.Type, RelationshipType.Friends));
    }
    
    public async Task DeclineFriendRequest(string requesterId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        
        var authenticatedUserId = await _userService.GetAuthenticatedUserId();
        
        await context.UserRelationships
            .Where(ur => ur.IdUser == requesterId && ur.IdRelatedUser == authenticatedUserId && ur.Type == RelationshipType.Pending)
            .ExecuteDeleteAsync();
    }
    
    public async Task RemoveFriend(string friendId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        
        var authenticatedUserId = await _userService.GetAuthenticatedUserId();
        
        await context.UserRelationships
            .Where(ur => ((ur.IdUser == authenticatedUserId && ur.IdRelatedUser == friendId) ||
                          (ur.IdUser == friendId && ur.IdRelatedUser == authenticatedUserId)) && ur.Type == RelationshipType.Friends)
            .ExecuteDeleteAsync();
    }

    public async Task<string?> GetFriendRequestMessage(string receiverId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        
        var authenticatedUserId = await _userService.GetAuthenticatedUserId();

        var request = await context.UserRelationships
            .Where(ur => ur.IdUser == authenticatedUserId && ur.IdRelatedUser == receiverId && ur.Type == RelationshipType.Pending)
            .Select(ur => ur.Message)
            .FirstOrDefaultAsync();
            
        return request;
    }
}