using System;
using System.Collections.Generic;

namespace expr2.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public string AdminName { get; set; } = null!;

    public string AdminPassword { get; set; } = null!;
}
