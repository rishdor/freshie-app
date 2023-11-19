using System;
using System.Collections.Generic;

namespace freshie_webAPI.Models;

public partial class Shelf
{
    public int ShelfId { get; set; }

    public string ShelfName { get; set; } = null!;

    //public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
