namespace s20601.Data.Models.DTOs;

public class ChatMessage
{
    public string IdSender { get; set; } = null!;
    public string IdRecipient { get; set; } = null!;
    public DateTime Created { get; init; }
    public string Content { get; set; } = null!;
    public MessageStatus MessageStatus { get; set; }
    public string SenderUsername { get; set; } = null!;
    public string RecipientUsername { get; set; } = null!;
    public string? SenderAvatar { get; set; }
    public string? RecipientAvatar { get; set; }
}