using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class V1
{
    public string? Ime { get; set; }

    public string Firma { get; set; } = null!;

    public int IdjobApplication { get; set; }

    public string Status { get; set; } = null!;
}
