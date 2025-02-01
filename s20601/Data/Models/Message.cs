using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class Message
{
    public int Id { get; set; }

    public int IdSender { get; set; }

    public int IdRecipient { get; set; }

    public int Created { get; set; }

    public string Content { get; set; } = null!;

    public int IdStatus { get; set; }

    public DateTime? DeliverTime { get; set; }

    public virtual User IdRecipientNavigation { get; set; } = null!;

    public virtual User IdSenderNavigation { get; set; } = null!;

    public virtual Status IdStatusNavigation { get; set; } = null!;
}
