using System;
using System.Collections.Generic;

namespace expr2.Models;

public partial class GroupMessage
{
    public int GroupMessageId { get; set; }

    public int? GroupId { get; set; }

    public int? SenderId { get; set; }

    public string? Content { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual Group? Group { get; set; }

    public virtual User? Sender { get; set; }
}
