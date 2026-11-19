using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Entities;
using OnlineLearningPlatform.Entities.Enums;

namespace OnlineLearningPlatform.Data;

public class AppDbContext : DbContext
{
    public DbSet<Instructor> Instructors => Set<Instructor>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=.;Database=OnlineLearningPlatformDB;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<Instructor>().HasData(
            new Instructor
            {
                InstructorId = 1,
                FullName = "Ahmed Hassan",
                Email = "ahmed@example.com",
                Bio = "Software Developer",
                JoinedDate = new DateTime(2024, 1, 1)
            },
            new Instructor
            {
                InstructorId = 2,
                FullName = "Sara Ali",
                Email = "sara@example.com",
                Bio = "Data Science Instructor",
                JoinedDate = new DateTime(2024, 2, 1)
            }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                CategoryId = 1,
                Name = "Programming"
            },
            new Category
            {
                CategoryId = 2,
                Name = "Web Development",
                ParentCategoryId = 1
            },
            new Category
            {
                CategoryId = 3,
                Name = "Database",
                ParentCategoryId = 1
            }
        );

        modelBuilder.Entity<Course>().HasData(
            new Course
            {
                CourseId = 1,
                Title = "C# Fundamentals",
                Price = 500,
                Level = CourseLevel.Beginner,
                IsPublished = true,
                PublishedDate = new DateTime(2024, 3, 1),
                InstructorId = 1,
                CategoryId = 2
            },
            new Course
            {
                CourseId = 2,
                Title = "SQL Server Basics",
                Price = 400,
                Level = CourseLevel.Intermediate,
                IsPublished = true,
                PublishedDate = new DateTime(2024, 4, 1),
                InstructorId = 2,
                CategoryId = 3
            }
        );

        modelBuilder.Entity<Student>().HasData(
            new Student
            {
                StudentId = 1,
                FullName = "Omar Mohamed",
                Email = "omar@example.com",
                DateOfBirth = new DateTime(2004, 5, 10)
            },
            new Student
            {
                StudentId = 2,
                FullName = "Mona Ali",
                Email = "mona@example.com",
                DateOfBirth = new DateTime(2003, 8, 15)
            }
        );

        modelBuilder.Entity<Enrollment>().HasData(
            new Enrollment
            {
                StudentId = 1,
                CourseId = 1,
                EnrollmentDate = new DateTime(2024, 5, 1),
                ProgressPercentage = 100,
                Status = EnrollmentStatus.Completed,
                FinalGrade = 95
            },
            new Enrollment
            {
                StudentId = 2,
                CourseId = 2,
                EnrollmentDate = new DateTime(2024, 5, 2),
                ProgressPercentage = 40,
                Status = EnrollmentStatus.Active
            }
        );
    }
}