using System;
using System.Collections.Generic;

namespace expr2.Models;

public partial class Friend
{
    public int UserId1 { get; set; }

    public int UserId2 { get; set; }

    public string? Status { get; set; }

    public DateTime? RequestDate { get; set; }

    public virtual User UserId1Navigation { get; set; } = null!;

    public virtual User UserId2Navigation { get; set; } = null!;
}
