using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int UserId { get; set; }

    public int GiftId { get; set; }

    public int NumberOfTickets { get; set; }

    public DateTime CaertDate { get; set; }

    public virtual Gift Gift { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
