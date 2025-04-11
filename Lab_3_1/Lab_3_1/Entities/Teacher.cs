using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Lab_3_1.Entities
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int Age { get; set; }

        public virtual ICollection<Course>? Courses { get; set; } = new List<Course>();
    }
}
