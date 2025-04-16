namespace s20601.Data.Models;

public partial class Message
{
    public int Id { get; set; }

    public string IdSender { get; set; } = null!;

    public string IdRecipient { get; set; } = null!;

    public int Created { get; set; }

    public string Content { get; set; } = null!;

    public int IdStatus { get; set; }

    public DateTime? DeliverTime { get; set; }

    public virtual ApplicationUser IdRecipientNavigation { get; set; } = null!;

    public virtual ApplicationUser IdSenderNavigation { get; set; } = null!;

    public virtual Status IdStatusNavigation { get; set; } = null!;
}
