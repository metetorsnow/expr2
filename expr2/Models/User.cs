using System;
using System.Collections.Generic;

namespace expr2.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? RegistrationDate { get; set; }

    public bool? IsApproved { get; set; }

    public bool? IsBanned { get; set; }

    public virtual ICollection<Friend> FriendUserId1Navigations { get; set; } = new List<Friend>();

    public virtual ICollection<Friend> FriendUserId2Navigations { get; set; } = new List<Friend>();

    public virtual ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();

    public virtual ICollection<GroupMessage> GroupMessages { get; set; } = new List<GroupMessage>();

    public virtual ICollection<Message> MessageReceivers { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();
}
