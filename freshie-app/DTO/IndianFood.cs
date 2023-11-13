using System;
using System.Collections.Generic;

namespace freshie_webAPI.Models;

public partial class IndianFood
{
    public int Id { get; set; }

    public string RecipeName { get; set; } = null!;

    public string TranslatedRecipeName { get; set; } = null!;

    public string? Ingredients { get; set; }

    public string? TranslatedIngredients { get; set; }

    public int PrepTimeInMins { get; set; }

    public int CookTimeInMins { get; set; }

    public int TotalTimeInMins { get; set; }

    public int Servings { get; set; }

    public string Cuisine { get; set; } = null!;

    public string Course { get; set; } = null!;

    public string Diet { get; set; } = null!;

    public string Instructions { get; set; } = null!;

    public string TranslatedInstructions { get; set; } = null!;

    public string Url { get; set; } = null!;
}
