using System;
using System.Collections.Generic;

namespace freshie_webAPI.Models;

public partial class ProductDiet
{
    public int ProductdietId { get; set; }

    public int ProductId { get; set; }

    public int DietId { get; set; }

    public virtual Diet Diet { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
