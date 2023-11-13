﻿using System;
using System.Collections.Generic;

namespace freshie_webAPI.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<FridgeItem> FridgeItems { get; set; } = new List<FridgeItem>();

    public virtual ICollection<GroceriesHistory> GroceriesHistories { get; set; } = new List<GroceriesHistory>();

    public virtual ICollection<GroceriesList> GroceriesLists { get; set; } = new List<GroceriesList>();
}
