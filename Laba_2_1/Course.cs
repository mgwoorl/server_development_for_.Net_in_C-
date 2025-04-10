using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba_2_1
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Duration { get; set; }
        public string? Description { get; set; }
    }
}
