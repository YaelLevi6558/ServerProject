using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Donor
{
    public int DonorId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Gift> Gifts { get; set; } = new List<Gift>();
}
