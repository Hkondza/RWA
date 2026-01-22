using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Firm
{
    public int Idfirm { get; set; }

    public string FirmName { get; set; } = null!;

    public string? Description { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? WebsiteUrl { get; set; }

    public int JobTypeId { get; set; }

    public virtual ICollection<JobOffer> JobOffers { get; set; } = new List<JobOffer>();

    public virtual JobType JobType { get; set; } = null!;

    public virtual ICollection<UserFirm> UserFirms { get; set; } = new List<UserFirm>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
