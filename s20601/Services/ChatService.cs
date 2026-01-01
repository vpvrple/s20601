using Microsoft.EntityFrameworkCore;
using s20601.Data;
using s20601.Data.Models;

namespace s20601.Services;

public class ChatService : IChatService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IUserService _userService;
    
    public ChatService(IDbContextFactory<ApplicationDbContext> dbContextFactory, IUserService userService)
    {
        _dbContextFactory = dbContextFactory;
        _userService = userService;
    }

    public async Task<Message?> SaveMessage(string idRecipient, string message)
    {
        var authenticatedUserId = await _userService.GetAuthenticatedUserId();

        if (authenticatedUserId == null)
        {
            return null;
        }
        
        using var context = await _dbContextFactory.CreateDbContextAsync();
        
        var messageDb = new Message()
        {
            IdSender = authenticatedUserId,
            IdRecipient = idRecipient,
            Content = message,
            Created = DateTime.Now,
            MessageStatus = MessageStatus.Sent
        };
        
        context.Messages.Add(messageDb);
        await context.SaveChangesAsync();

        return messageDb;
    }

    public async Task<List<Message>?> GetConversation(string friendId)
    {
        var authenticatedUserId = await _userService.GetAuthenticatedUserId();
        
        if (authenticatedUserId == null)
        {
            return null;
        }
        
        using var context = await _dbContextFactory.CreateDbContextAsync();
        
        return await context.Messages
            .Where(m => (m.IdSender == authenticatedUserId && m.IdRecipient == friendId) || 
                        (m.IdSender == friendId && m.IdRecipient == authenticatedUserId))
            .OrderBy(m => m.Created)
            .ToListAsync();
    }
}
