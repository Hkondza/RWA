using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class User
{
    public int Iduser { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public int? FirmId { get; set; }

    public virtual Firm? Firm { get; set; }

    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();

    public virtual ICollection<UserFirm> UserFirms { get; set; } = new List<UserFirm>();
}
