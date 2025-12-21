using Microsoft.EntityFrameworkCore;
using s20601.Data;
using s20601.Data.Models;

namespace s20601.Services;

public class ChatService : IChatService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    
    public ChatService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<Message> SaveMessage(string IdSender, string IdRecipient, string message)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var messageDb = new Message()
        {
            IdSender = IdSender,
            IdRecipient = IdRecipient,
            Content = message,
            Created = DateTime.Now,
            MessageStatus = MessageStatus.Sent
        };
        
        context.Messages.Add(messageDb);
        await context.SaveChangesAsync();

        return messageDb;
    }

    public async Task<List<Message>> GetConversation(string userId1, string userId2)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Messages
            .Where(m => (m.IdSender == userId1 && m.IdRecipient == userId2) || 
                        (m.IdSender == userId2 && m.IdRecipient == userId1))
            .OrderBy(m => m.Created)
            .ToListAsync();
    }
}
