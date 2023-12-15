using System;
using System.Collections.Generic;

namespace freshiehubAPI.Models;

public partial class GroceriesList
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int? UserId { get; set; }
}
