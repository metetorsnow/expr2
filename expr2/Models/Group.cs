using System;
using System.Collections.Generic;

namespace expr2.Models;

public partial class Group
{
    public int GroupId { get; set; }

    public string GroupName { get; set; } = null!;

    public DateTime? CreationDate { get; set; }

    public virtual ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();

    public virtual ICollection<GroupMessage> GroupMessages { get; set; } = new List<GroupMessage>();
}
