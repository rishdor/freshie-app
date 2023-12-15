using System;
using System.Collections.Generic;

namespace freshiehubAPI.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    //public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
