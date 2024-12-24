using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Purchase { 
    public int PurchaseId { get; set; }

    public int GiftId { get; set; }

    public int NumberOfTickets { get; set; }

    public DateTime PurchaseDate { get; set; }

    public int? UserId { get; set; }

    public virtual Gift Gift { get; set; } = null!;

    public virtual User? User { get; set; }

    public virtual ICollection<Winner> Winners { get; set; } = new List<Winner>();
}
