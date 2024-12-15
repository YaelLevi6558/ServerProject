using System;
using System.Collections.Generic;
namespace Server.Models;
public partial class Purchase
{
    public int PurchaseId { get; set; }

    public int GiftId { get; set; }

    public string BuyerName { get; set; } = null!;

    public string BuyerEmail { get; set; } = null!;

    public int NumberOfTickets { get; set; }

    public DateTime PurchaseDate { get; set; }

    public decimal BuyerPhone { get; set; }

    public virtual Gift Gift { get; set; } = null!;
}
