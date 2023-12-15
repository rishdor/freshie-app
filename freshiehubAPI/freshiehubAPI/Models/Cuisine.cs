using System;
using System.Collections.Generic;

namespace freshiehubAPI.Models;

public partial class Cuisine
{
    public int CuisineId { get; set; }

    public string CuisineName { get; set; } = null!;

    //public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
