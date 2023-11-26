using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace freshie_webAPI.Models
{
    [Table("Course")]
        public class Course
        {
            [Key]
            [Column("course_id")]
            public int CourseId { get; set; }
            [Column("course_name")]
            public string CourseName { get; set; } = null!;
        }
 
}
