using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Gift> Gifts { get; set; } = new List<Gift>();
}
