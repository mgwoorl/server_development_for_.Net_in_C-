using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration.Json;

namespace Laba_2_1
{
    public class AppContext : DbContext
    {
        public AppContext()
        {
        }

        public AppContext(DbContextOptions<AppContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .Build();

            optionsBuilder.UseSqlite(config.GetConnectionString("DefaultConnection"));
        }

        public void SeedData()
        {
            if (!Courses.Any())
            {
                Course c1 = new Course { Id = 1, Title = "Основы программирования", Duration = 30, Description = "Курс вводит в основы программирования на языке C#." };
                Course c2 = new Course { Id = 2, Title = "Физика", Duration = 45, Description = "Курс по физике." };
                Course c3 = new Course { Id = 3, Title = "Математика", Duration = 30, Description = "Курс по математике." };

                Courses.Add(c1);
                Courses.Add(c2);
                Courses.Add(c3);

                SaveChanges();
            }
        }

        public void UpdateCourse(int courseId, string newTitle, int newDuration, string? newDescription)
        {
            var course = Courses.Find(courseId);
            if (course != null)
            {
                course.Title = newTitle;
                course.Duration = newDuration;
                course.Description = newDescription;
                SaveChanges();
            }
        }
    }
}