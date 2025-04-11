using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab_3_1.Entities
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        public string? CourseName { get; set; }
        public string Title { get; set; } = null!;
        public int Duration { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Student>? Students { get; set; } = new List<Student>();
        public virtual ICollection<Teacher>? Teachers { get; set; } = new List<Teacher>();
    }
}
