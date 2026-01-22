using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class JobType
{
    public int IdjobType { get; set; }

    public string JobName { get; set; } = null!;

    public virtual ICollection<Firm> Firms { get; set; } = new List<Firm>();

    public virtual ICollection<JobOffer> JobOffers { get; set; } = new List<JobOffer>();
}
