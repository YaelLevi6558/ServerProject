using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string UserFirstName { get; set; } = null!;

    public string UserLastName { get; set; } = null!;

    public string? UserPhone { get; set; }

    public string UserEmail { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}
