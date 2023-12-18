﻿using System;
using System.Collections.Generic;

namespace freshiehubAPI.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Icon { get; set; }

    public int? CategoryId { get; set; }

    public int? ShelfId { get; set; }

    //public virtual Category? Category { get; set; }

    //public virtual ICollection<FridgeItem> FridgeItems { get; set; } = new List<FridgeItem>();

    //public virtual ICollection<GroceriesHistory> GroceriesHistories { get; set; } = new List<GroceriesHistory>();

    //public virtual ICollection<GroceriesList> GroceriesLists { get; set; } = new List<GroceriesList>();

    //public virtual Shelf? Shelf { get; set; }
}
