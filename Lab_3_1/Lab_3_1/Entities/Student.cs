using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab_3_1.Entities
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<Course>? Courses { get; set; } = new List<Course>();
    }
}
