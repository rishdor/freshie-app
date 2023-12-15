using System;
using System.Collections.Generic;

namespace freshiehubAPI.Models;

public partial class Recipe
{
    public int RecipeId { get; set; }

    public string RecipeName { get; set; } = null!;

    public string? Ingredients { get; set; }

    public int PrepTimeInMins { get; set; }

    public int CookTimeInMins { get; set; }

    public int TotalTimeInMins { get; set; }

    public int Servings { get; set; }

    public int CuisineId { get; set; }

    public int CourseId { get; set; }

    public int DietId { get; set; }

    public string Instructions { get; set; } = null!;

    public string Url { get; set; } = null!;

    //public virtual Course Course { get; set; } = null!;

    //public virtual Cuisine Cuisine { get; set; } = null!;

    //public virtual Diet Diet { get; set; } = null!;
}
