using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Location
{
    public int Idlocation { get; set; }

    public string LocationName { get; set; } = null!;

    public virtual ICollection<JobOffer> JobOffers { get; set; } = new List<JobOffer>();
}
