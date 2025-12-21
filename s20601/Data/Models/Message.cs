namespace s20601.Data.Models;

public class Message
{
    public int Id { get; set; }

    public string IdSender { get; set; } = null!;

    public string IdRecipient { get; set; } = null!;

    public DateTime Created { get; set; }

    public string Content { get; set; } = null!;

    public MessageStatus MessageStatus { get; set; }

    public virtual ApplicationUser IdRecipientNavigation { get; set; } = null!;

    public virtual ApplicationUser IdSenderNavigation { get; set; } = null!;
}

public enum MessageStatus
{
    Sent,
    Read,
    Unread
}
