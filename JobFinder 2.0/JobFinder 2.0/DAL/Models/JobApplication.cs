using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class JobApplication
{
    public int IdjobApplication { get; set; }

    public int JobOfferId { get; set; }

    public int UserId { get; set; }

    public string? Message { get; set; }

    public string Status { get; set; } = null!;

    public DateTime AppliedAt { get; set; }

    public virtual JobOffer JobOffer { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();
}
