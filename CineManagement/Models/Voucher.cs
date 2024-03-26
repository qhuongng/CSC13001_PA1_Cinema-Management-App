using System;
using System.Collections.Generic;

namespace CineManagement.Models;

public partial class Voucher
{
    public int VoucherId { get; set; }

    public int UserId { get; set; }

    public bool IsUsed { get; set; }

    public int DiscountPercent { get; set; }

    public virtual User User { get; set; } = null!;
}
