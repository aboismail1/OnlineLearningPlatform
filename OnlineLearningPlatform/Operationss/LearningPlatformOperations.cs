using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.Entities;
using OnlineLearningPlatform.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace OnlineLearningPlatform.Operations;

public class LearningPlatformOperations
{
    private readonly AppDbContext _context;

    public LearningPlatformOperations()
    {
        _context = new AppDbContext();
    }


    public void AddInstructor()
    {
        Console.WriteLine("\n=== Add New Instructor ===");

        Console.Write("Enter Full Name: ");
        string? name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Name cannot be empty.");
            return;
        }

        Console.Write("Enter Email: ");
        string? email = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(email))
        {
            Console.WriteLine("Email cannot be empty.");
            return;
        }

        Console.Write("Enter Bio (optional): ");
        string? bio = Console.ReadLine();

        var instructor = new Instructor
        {
            FullName = name,
            Email = email,
            Bio = string.IsNullOrWhiteSpace(bio) ? null : bio,
            JoinedDate = DateTime.Now
        };

        _context.Instructors.Add(instructor);
        _context.SaveChanges();

        Console.WriteLine(
            $"Instructor added successfully! ID: {instructor.InstructorId}");
    }


    public void AddCourse()
    {
        Console.WriteLine("\n=== Add New Course ===");

        Console.Write("Enter Course Title: ");
        string? title = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Title cannot be empty.");
            return;
        }

        Console.Write("Enter Description: ");
        string? description = Console.ReadLine();

        Console.Write("Enter Price: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 0)
        {
            Console.WriteLine("Invalid price.");
            return;
        }

        Console.WriteLine("Choose Course Level: 0-Beginner, 1-Intermediate, 2-Advanced");
        if (!int.TryParse(Console.ReadLine(), out int level) ||
            !Enum.IsDefined(typeof(CourseLevel), level))
        {
            Console.WriteLine("Invalid course level.");
            return;
        }

        Console.Write("Enter Instructor ID: ");
        if (!int.TryParse(Console.ReadLine(), out int instructorId))
        {
            Console.WriteLine("Invalid Instructor ID.");
            return;
        }

        Console.Write("Enter Category ID: ");
        if (!int.TryParse(Console.ReadLine(), out int categoryId))
        {
            Console.WriteLine("Invalid Category ID.");
            return;
        }

        bool instructorExists = _context.Instructors.Any(i => i.InstructorId == instructorId);
        bool categoryExists = _context.Categories.Any(c => c.CategoryId == categoryId);

        if (!instructorExists)
        {
            Console.WriteLine("Instructor not found.");
            return;
        }

        if (!categoryExists)
        {
            Console.WriteLine("Category not found.");
            return;
        }

        var course = new Course
        {
            Title = title,
            Description = description,
            Price = price,
            Level = (CourseLevel)level,
            IsPublished = false,
            PublishedDate = null,
            InstructorId = instructorId,
            CategoryId = categoryId
        };

        _context.Courses.Add(course);
        _context.SaveChanges();

        Console.WriteLine($"Course added successfully! ID: {course.CourseId}");
    }


    public void AddStudents()
    {
        Console.WriteLine("\n=== Add 3 New Students ===");

        var students = new List<Student>();

        for (int i = 1; i <= 3; i++)
        {
            Console.WriteLine($"\nStudent {i}");

            Console.Write("Enter Full Name: ");
            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return;
            }

            Console.Write("Enter Email: ");
            string? email = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Email cannot be empty.");
                return;
            }

            Console.Write("Enter Date of Birth (yyyy-MM-dd): ");

            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dateOfBirth))
            {
                Console.WriteLine("Invalid date.");
                return;
            }

            students.Add(new Student
            {
                FullName = name,
                Email = email,
                DateOfBirth = dateOfBirth
            });
        }

        _context.Students.AddRange(students);
        _context.SaveChanges();

        Console.WriteLine("\nStudents added successfully!");

        foreach (var student in students)
        {
            Console.WriteLine($"ID: {student.StudentId} - {student.FullName}");
        }
    }


    public void EnrollStudent()
    {
        Console.WriteLine("\n=== Enroll Student in Course ===");

        Console.Write("Enter Student ID: ");

        if (!int.TryParse(Console.ReadLine(), out int studentId))
        {
            Console.WriteLine("Invalid Student ID.");
            return;
        }

        Console.Write("Enter Course ID: ");

        if (!int.TryParse(Console.ReadLine(), out int courseId))
        {
            Console.WriteLine("Invalid Course ID.");
            return;
        }

        var student = _context.Students.Find(studentId);

        if (student == null)
        {
            Console.WriteLine("Student not found.");
            return;
        }

        var course = _context.Courses.Find(courseId);

        if (course == null)
        {
            Console.WriteLine("Course not found.");
            return;
        }

        if (!course.IsPublished)
        {
            Console.WriteLine("Cannot enroll in an unpublished course.");
            return;
        }

        bool alreadyEnrolled = _context.Enrollments.Any(e =>
            e.StudentId == studentId && e.CourseId == courseId);

        if (alreadyEnrolled)
        {
            Console.WriteLine("Student is already enrolled in this course.");
            return;
        }

        var enrollment = new Enrollment
        {
            StudentId = studentId,
            CourseId = courseId,
            EnrollmentDate = DateTime.Now,
            ProgressPercentage = 0,
            Status = EnrollmentStatus.Active,
            FinalGrade = null
        };

        _context.Enrollments.Add(enrollment);
        _context.SaveChanges();

        Console.WriteLine("Student enrolled successfully!");
    }



    public void FindCourseById()
    {
        Console.WriteLine("\n=== Find Course By ID ===");

        Console.Write("Enter Course ID: ");

        if (!int.TryParse(Console.ReadLine(), out int courseId))
        {
            Console.WriteLine("Invalid Course ID.");
            return;
        }

        var course = _context.Courses.Find(courseId);

        if (course == null)
        {
            Console.WriteLine("Course not found.");
            return;
        }

        Console.WriteLine($"ID: {course.CourseId}");
        Console.WriteLine($"Title: {course.Title}");
        Console.WriteLine($"Price: {course.Price}");
        Console.WriteLine($"Level: {course.Level}");
        Console.WriteLine($"Published: {course.IsPublished}");
    }


    public void FindCourseByTitle()
    {
        Console.WriteLine("\n=== Find Course By Title ===");

        Console.Write("Enter Course Title: ");
        string? title = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Title cannot be empty.");
            return;
        }

        var course = _context.Courses.FirstOrDefault(c => c.Title == title);

        if (course == null)
        {
            Console.WriteLine("Course not found.");
            return;
        }

        Console.WriteLine($"ID: {course.CourseId}");
        Console.WriteLine($"Title: {course.Title}");
        Console.WriteLine($"Price: {course.Price}");
        Console.WriteLine($"Level: {course.Level}");
        Console.WriteLine($"Published: {course.IsPublished}");
    }



    public void FilterCoursesByPrice()
    {
        Console.WriteLine("\n=== Filter Courses By Price ===");

        Console.Write("Enter Minimum Price: ");

        if (!decimal.TryParse(Console.ReadLine(), out decimal minPrice))
        {
            Console.WriteLine("Invalid minimum price.");
            return;
        }

        Console.Write("Enter Maximum Price: ");

        if (!decimal.TryParse(Console.ReadLine(), out decimal maxPrice))
        {
            Console.WriteLine("Invalid maximum price.");
            return;
        }

        if (minPrice < 0 || maxPrice < minPrice)
        {
            Console.WriteLine("Invalid price range.");
            return;
        }

        var courses = _context.Courses
            .Where(c => c.Price >= minPrice && c.Price <= maxPrice)
            .OrderByDescending(c => c.Price)
            .ToList();

        if (courses.Count == 0)
        {
            Console.WriteLine("No courses found in this price range.");
            return;
        }

        foreach (var course in courses)
        {
            Console.WriteLine(
                $"Title: {course.Title} | Level: {course.Level} | Price: {course.Price}");
        }
    }



    public void GetSubCategories()
    {
        Console.WriteLine("\n=== Get Subcategories ===");

        Console.Write("Enter Parent Category ID: ");

        if (!int.TryParse(Console.ReadLine(), out int categoryId))
        {
            Console.WriteLine("Invalid Category ID.");
            return;
        }

        bool categoryExists = _context.Categories.Any(c => c.CategoryId == categoryId);

        if (!categoryExists)
        {
            Console.WriteLine("Category not found.");
            return;
        }

        var subCategories = _context.Categories
            .Where(c => c.ParentCategoryId == categoryId)
            .ToList();

        if (subCategories.Count == 0)
        {
            Console.WriteLine("This category has no subcategories.");
            return;
        }

        foreach (var category in subCategories)
        {
            Console.WriteLine($"ID: {category.CategoryId} | Name: {category.Name}");
        }
    }



    public void GetTopLevelCategories()
    {
        Console.WriteLine("\n=== Top-Level Categories ===");

        var categories = _context.Categories
            .Where(c => c.ParentCategoryId == null)
            .ToList();

        if (categories.Count == 0)
        {
            Console.WriteLine("No top-level categories found.");
            return;
        }

        foreach (var category in categories)
        {
            Console.WriteLine($"ID: {category.CategoryId} | Name: {category.Name}");
        }
    }



    public void GetCourseRoster()
    {
        Console.WriteLine("\n=== Get Course Roster ===");

        Console.Write("Enter Course ID: ");

        if (!int.TryParse(Console.ReadLine(), out int courseId))
        {
            Console.WriteLine("Invalid Course ID.");
            return;
        }

        var course = _context.Courses.Find(courseId);

        if (course == null)
        {
            Console.WriteLine("Course not found.");
            return;
        }

        var studentIds = _context.Enrollments
            .Where(e => e.CourseId == courseId)
            .Select(e => e.StudentId)
            .ToList();

        Console.WriteLine($"Course: {course.Title}");
        Console.WriteLine($"Total Enrollments: {studentIds.Count}");

        if (studentIds.Count == 0)
        {
            Console.WriteLine("No students enrolled in this course.");
            return;
        }

        var students = _context.Students
            .Where(s => studentIds.Contains(s.StudentId))
            .ToList();

        Console.WriteLine("\nEnrolled Students:");

        foreach (var student in students)
        {
            Console.WriteLine($"ID: {student.StudentId} | Name: {student.FullName}");
        }
    }


    public void UpdateCoursePrice()
    {
        Console.WriteLine("\n=== Update Course Price ===");

        Console.Write("Enter Course ID: ");

        if (!int.TryParse(Console.ReadLine(), out int courseId))
        {
            Console.WriteLine("Invalid Course ID.");
            return;
        }

        var course = _context.Courses.Find(courseId);

        if (course == null)
        {
            Console.WriteLine("Course not found.");
            return;
        }

        Console.WriteLine($"Current Price: {course.Price}");

        Console.Write("Enter New Price: ");

        if (!decimal.TryParse(Console.ReadLine(), out decimal newPrice) || newPrice < 0)
        {
            Console.WriteLine("Invalid price.");
            return;
        }

        course.Price = newPrice;

        _context.SaveChanges();

        Console.WriteLine("Course price updated successfully!");
    }



    public void UpdateEnrollmentProgress()
    {
        Console.WriteLine("\n=== Update Enrollment Progress ===");

        Console.Write("Enter Student ID: ");

        if (!int.TryParse(Console.ReadLine(), out int studentId))
        {
            Console.WriteLine("Invalid Student ID.");
            return;
        }

        Console.Write("Enter Course ID: ");

        if (!int.TryParse(Console.ReadLine(), out int courseId))
        {
            Console.WriteLine("Invalid Course ID.");
            return;
        }

        var enrollment = _context.Enrollments.Find(studentId, courseId);

        if (enrollment == null)
        {
            Console.WriteLine("Enrollment not found.");
            return;
        }

        Console.Write("Enter New Progress Percentage: ");

        if (!decimal.TryParse(Console.ReadLine(), out decimal progress) ||
            progress < 0 || progress > 100)
        {
            Console.WriteLine("Progress must be between 0 and 100.");
            return;
        }

        enrollment.ProgressPercentage = progress;

        if (progress == 100)
        {
            enrollment.Status = EnrollmentStatus.Completed;
        }

        _context.SaveChanges();

        Console.WriteLine("Enrollment progress updated successfully!");
    }


    public void PublishCourse()
    {
        Console.WriteLine("\n=== Publish Course ===");

        Console.Write("Enter Course ID: ");

        if (!int.TryParse(Console.ReadLine(), out int courseId))
        {
            Console.WriteLine("Invalid Course ID.");
            return;
        }

        var course = _context.Courses.Find(courseId);

        if (course == null)
        {
            Console.WriteLine("Course not found.");
            return;
        }

        if (course.IsPublished)
        {
            Console.WriteLine("Course is already published.");
            return;
        }

        course.IsPublished = true;
        course.PublishedDate = DateTime.Now;

        _context.SaveChanges();

        Console.WriteLine("Course published successfully!");
    }



    public void UnenrollStudent()
    {
        Console.WriteLine("\n=== Unenroll Student ===");

        Console.Write("Enter Student ID: ");

        if (!int.TryParse(Console.ReadLine(), out int studentId))
        {
            Console.WriteLine("Invalid Student ID.");
            return;
        }

        Console.Write("Enter Course ID: ");

        if (!int.TryParse(Console.ReadLine(), out int courseId))
        {
            Console.WriteLine("Invalid Course ID.");
            return;
        }

        var enrollment = _context.Enrollments.Find(studentId, courseId);

        if (enrollment == null)
        {
            Console.WriteLine("Enrollment not found.");
            return;
        }

        _context.Enrollments.Remove(enrollment);
        _context.SaveChanges();

        Console.WriteLine("Student unenrolled successfully!");
    }


    public void DeleteCategory()
    {
        Console.WriteLine("\n=== Delete Category ===");

        Console.Write("Enter Category ID to test deletion with subcategories: ");

        if (!int.TryParse(Console.ReadLine(), out int categoryId))
        {
            Console.WriteLine("Invalid Category ID.");
            return;
        }

        var category = _context.Categories.Find(categoryId);

        if (category == null)
        {
            Console.WriteLine("Category not found.");
            return;
        }

        try
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();

            Console.WriteLine("Category deleted successfully.");
        }
        catch (DbUpdateException)
        {
            Console.WriteLine("Cannot delete this category because it has subcategories or related courses.");

            _context.Entry(category).State = EntityState.Unchanged;
        }

        Console.WriteLine("\n=== Safe Category Deletion ===");

        Console.Write("Enter Category ID to delete safely: ");

        if (!int.TryParse(Console.ReadLine(), out int safeCategoryId))
        {
            Console.WriteLine("Invalid Category ID.");
            return;
        }

        var safeCategory = _context.Categories.Find(safeCategoryId);

        if (safeCategory == null)
        {
            Console.WriteLine("Category not found.");
            return;
        }

        bool hasSubCategories = _context.Categories
            .Any(c => c.ParentCategoryId == safeCategoryId);

        if (hasSubCategories)
        {
            Console.WriteLine("Cannot delete category because it has subcategories.");
            return;
        }

        bool hasCourses = _context.Courses
            .Any(c => c.CategoryId == safeCategoryId);

        if (hasCourses)
        {
            Console.WriteLine("Cannot delete category because it has courses.");
            return;
        }

        _context.Categories.Remove(safeCategory);
        _context.SaveChanges();

        Console.WriteLine("Category deleted successfully!");
    }



    public void DeleteCourseWithEnrollments()
    {
        Console.WriteLine("\n=== Delete Course With Enrollments ===");

        Console.Write("Enter Course ID: ");

        if (!int.TryParse(Console.ReadLine(), out int courseId))
        {
            Console.WriteLine("Invalid Course ID.");
            return;
        }

        var course = _context.Courses.Find(courseId);

        if (course == null)
        {
            Console.WriteLine("Course not found.");
            return;
        }

        int beforeCount = _context.Enrollments
            .Count(e => e.CourseId == courseId);

        Console.WriteLine($"Enrollments before deletion: {beforeCount}");

        _context.Courses.Remove(course);
        _context.SaveChanges();

        int afterCount = _context.Enrollments
            .Count(e => e.CourseId == courseId);

        Console.WriteLine($"Enrollments after deletion: {afterCount}");
        Console.WriteLine("Course deleted successfully!");
    }
}