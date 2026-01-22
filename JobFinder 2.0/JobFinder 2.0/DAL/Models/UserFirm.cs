using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class UserFirm
{
    public int IduserFirm { get; set; }

    public int UserId { get; set; }

    public int FirmId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime RequestedAt { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public virtual Firm Firm { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
