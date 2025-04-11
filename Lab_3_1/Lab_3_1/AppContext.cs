using Microsoft.EntityFrameworkCore;
using Lab_3_1.Entities;

public class ApplicationContext : DbContext
{
    public DbSet<Student> Students { get; set; } = null!;

    public DbSet<Teacher> Teachers { get; set; } = null!;

    public DbSet<Course> Courses { get; set; } = null!;


    public ApplicationContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        //Database.EnsureDeleted(); // Опционально, если нужно пересоздавать базу
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .Build();

        optionsBuilder.UseSqlite(config.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Многие ко многим между Course и Teacher
        modelBuilder.Entity<Course>()
            .HasMany(c => c.Teachers)
            .WithMany(t => t.Courses)
            .UsingEntity(j => j.ToTable("CourseTeachers"));

        modelBuilder.Entity<CourseTeacher>()
            .HasKey(ct => new { ct.CourseId, ct.TeacherId });

        modelBuilder.Entity<CourseTeacher>()
            .HasOne(ct => ct.Course)
            .WithMany()
            .HasForeignKey(ct => ct.CourseId);

        modelBuilder.Entity<CourseTeacher>()
            .HasOne(ct => ct.Teacher)
            .WithMany()
            .HasForeignKey(ct => ct.TeacherId);


        // Многие ко многим между Course и Student
        modelBuilder.Entity<Course>()
            .HasMany(c => c.Students)
            .WithMany(s => s.Courses)
            .UsingEntity(j => j.ToTable("CourseStudents"));

        modelBuilder.Entity<CourseStudent>()
            .HasKey(cs => new { cs.CourseId, cs.StudentId });

        modelBuilder.Entity<CourseStudent>()
            .HasOne(cs => cs.Course)
            .WithMany()
            .HasForeignKey(cs => cs.CourseId);

        modelBuilder.Entity<CourseStudent>()
            .HasOne(cs => cs.Student)
            .WithMany()
            .HasForeignKey(cs => cs.StudentId);
    }

    public void SeedData()
    {
        if (!Students.Any() && !Courses.Any() && !Teachers.Any())
        {
            Student s1 = new Student { FirstName = "Катя", LastName = "Конова", Age = 20, Address = "Moskow" };
            Student s2 = new Student { FirstName = "Иван", LastName = "Иванов", Age = 25, Address = "Novosibirsk" };
            Student s3 = new Student { FirstName = "Кира", LastName = "Петрова", Age = 23, Address = "Irkutsk" };

            Students.Add(s1);
            Students.Add(s2);
            Students.Add(s3);

            Course c1 = new Course { CourseId = 1, Title = "Основы программирования", Duration = 30, Description = "Курс вводит в основы программирования на языке C#." };
            Course c2 = new Course { CourseId = 2, Title = "Физика", Duration = 45, Description = "Курс по физике." };
            Course c3 = new Course { CourseId = 3, Title = "Математика", Duration = 30, Description = "Курс по математике." };

            Courses.Add(c1);
            Courses.Add(c2);

            Teacher t1 = new Teacher { TeacherId = 1, FirstName = "Елена", LastName = "Сергеева", Age = 35 };
            Teacher t2 = new Teacher { TeacherId = 2, FirstName = "Олег", LastName = "Краснов", Age = 50 };

            Teachers.Add(t1);
            Teachers.Add(t2);

            t1.Courses.Add(c1);
            t1.Courses.Add(c3);
            t2.Courses.Add(c1);
            t2.Courses.Add(c2);

            s1.Courses.Add(c1);
            s2.Courses.Add(c1);
            s2.Courses.Add(c2);
            s3.Courses.Add(c3);

            SaveChanges();
        }
    }
}
