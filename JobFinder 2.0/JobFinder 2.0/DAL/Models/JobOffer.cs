using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class JobOffer
{
    public int IdjobOffer { get; set; }

    public int FirmId { get; set; }

    public int JobTypeId { get; set; }

    public int LocationId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? Salary { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Firm Firm { get; set; } = null!;

    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();

    public virtual JobType JobType { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;
}
