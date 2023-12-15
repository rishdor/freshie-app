using System;
using System.Collections.Generic;

namespace freshiehubAPI.Models;

public partial class Diet
{
    public int DietId { get; set; }

    public string DietName { get; set; } = null!;

    //public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
