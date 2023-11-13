using System;
using System.Collections.Generic;

namespace freshie_webAPI.Models;

public partial class FridgeItem
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int UserId { get; set; }

    public DateTime? ExpirationDate { get; set; }

    //public virtual Product? Product { get; set; }

    //public virtual User User { get; set; } = null!;
}
