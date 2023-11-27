using System.ComponentModel.DataAnnotations.Schema;

namespace freshie_webAPI.Models
{
    [Table("Recipes")]
    public class Recipe
    {
        [Column("recipe_id")]
        public int RecipeId { get; set; }
      [Column("recipe_name")]
        public string RecipeName { get; set; }
      [Column("ingredients")]
        public string Ingridients { get; set; }
      [Column("PrepTimeInMins")]
        public int PrepTimeInMins { get; set; }
        [Column("CookTimeInMins")]
        public int CookTimeInMins { get; set; }
        [Column("TotalTimeInMins")]
        public int TotalTimeInMins { get; set; }
        [Column("Servings")]
        public int Servings { get; set; }
        [Column("cuisine_id")]
        public int CuisineId { get; set; }
        [Column("course_id")]
        public int CourseId { get; set; }
        [Column("diet_id")]
        public int DietId { get; set; }
        [Column("instructions")]
        public string Instructions { get; set; }
        [Column("URL")]
        public string URL { get; set; }
    }
}
