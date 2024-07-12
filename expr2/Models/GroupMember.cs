using System;
using System.Collections.Generic;

namespace expr2.Models;

public partial class GroupMember
{
    public int GroupId { get; set; }

    public int UserId { get; set; }

    public DateTime? JoinedDate { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
