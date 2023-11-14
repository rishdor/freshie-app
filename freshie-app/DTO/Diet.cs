using System;
using System.Collections.Generic;

namespace freshie_app.DTO;

public partial class Diet
{
    public int DietId { get; set; }

    public string DietName { get; set; } = null!;

    public virtual ICollection<ProductDiet> ProductDiets { get; set; } = new List<ProductDiet>();
}
