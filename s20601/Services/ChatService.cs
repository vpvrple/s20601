using Microsoft.EntityFrameworkCore;
using s20601.Data;
using s20601.Data.Models;
using s20601.Data.Models.DTOs;

namespace s20601.Services;

public class ChatService : IChatService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly ICurrentUserService _currentUserService;

    public ChatService(IDbContextFactory<ApplicationDbContext> dbContextFactory, ICurrentUserService currentCurrentUserService)
    {
        _dbContextFactory = dbContextFactory;
        _currentUserService = currentCurrentUserService;
    }

    public async Task<Message?> SaveMessage(string idRecipient, string message)
    {
        var authenticatedUserId = await _currentUserService.GetAuthenticatedUserId();

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
            Created = DateTime.UtcNow,
            MessageStatus = MessageStatus.Sent
        };

        context.Messages.Add(messageDb);
        await context.SaveChangesAsync();

        return messageDb;
    }

    public async Task<List<ChatMessage>?> GetConversation(string friendId)
    {
        var authenticatedUserId = await _currentUserService.GetAuthenticatedUserId();

        if (authenticatedUserId == null)
        {
            return null;
        }

        using var context = await _dbContextFactory.CreateDbContextAsync();

        return await context.Messages
            .Include(x => x.IdRecipientNavigation)
            .Include(x => x.IdSenderNavigation)
            .Where(m => (m.IdSender == authenticatedUserId && m.IdRecipient == friendId) ||
                        (m.IdSender == friendId && m.IdRecipient == authenticatedUserId))
            .Select(x => new ChatMessage()
            {
                IdSender = x.IdSender,
                IdRecipient = x.IdRecipient,
                Created = x.Created,
                Content = x.Content,
                MessageStatus = x.MessageStatus,
                SenderUsername = x.IdSenderNavigation.UserName,
                RecipientUsername = x.IdRecipientNavigation.UserName,
                SenderAvatar = x.IdSenderNavigation.Avatar,
                RecipientAvatar = x.IdRecipientNavigation.Avatar,
            })
            .OrderBy(m => m.Created)
            .ToListAsync();
    }
}
