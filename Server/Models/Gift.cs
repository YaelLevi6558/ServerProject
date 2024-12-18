using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Gift
{
    public int GiftId { get; set; }

    public string GiftName { get; set; } = null!;

    public int CategoryId { get; set; }

    public int DonorId { get; set; }

    public decimal TicketCost { get; set; }

    public string? ImageUrl { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Donor Donor { get; set; } = null!;

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual ICollection<Winner> Winners { get; set; } = new List<Winner>();
}
