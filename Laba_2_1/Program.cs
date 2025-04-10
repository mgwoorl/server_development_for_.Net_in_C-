using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Laba_2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new AppContext())
            {
                db.Database.EnsureCreated();

                db.SeedData();

                Console.WriteLine("Список курсов:");
                foreach (var course in db.Courses)
                {
                    Console.WriteLine($"  Id: {course.Id}, Title: {course.Title}, Duration: {course.Duration}, Description: {course.Description}");
                }

                int courseIdToUpdate = 1;
                string newTitle = "Основы C# программирования";
                int newDuration = 90;
                string newDescription = "Обновленный курс, вводящий в основы C#.";

                db.UpdateCourse(courseIdToUpdate, newTitle, newDuration, newDescription);
                Console.WriteLine($"\nКурс с Id = {courseIdToUpdate} обновлен.");

                Console.WriteLine("\nОбновленный список курсов:");
                foreach (var course in db.Courses)
                {
                    Console.WriteLine($"  Id: {course.Id}, Title: {course.Title}, Duration: {course.Duration}, Description: {course.Description}");
                }
            }
        }
    }
}