using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Worker
{
    public int Idworker { get; set; }

    public int JobApplicationId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime WorkStartedAt { get; set; }

    public DateTime? WorkFinishedAt { get; set; }

    public virtual JobApplication JobApplication { get; set; } = null!;
}
