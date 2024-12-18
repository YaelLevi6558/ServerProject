using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Winner
{
    public int WinnerId { get; set; }

    public int GiftId { get; set; }

    public string WinnerName { get; set; } = null!;

    public string WinnerEmail { get; set; } = null!;

    public decimal WinnerPhone { get; set; }

    public DateTime WinningDate { get; set; }

    public int? PurchaseId { get; set; }

    public virtual Gift Gift { get; set; } = null!;

    public virtual Purchase? Purchase { get; set; }
}
