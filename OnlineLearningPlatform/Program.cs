
using OnlineLearningPlatform.Operations;

var operations = new LearningPlatformOperations();

while (true)
{
    Console.WriteLine("\n========== Online Learning Platform ==========");
    Console.WriteLine("1. Add Instructor");
    Console.WriteLine("2. Add Course");
    Console.WriteLine("3. Add 3 Students");
    Console.WriteLine("4. Enroll Student");

    Console.WriteLine("\n--- Read Operations ---");
    Console.WriteLine("5. Find Course By ID");
    Console.WriteLine("6. Find Course By Title");
    Console.WriteLine("7. Filter Courses By Price");
    Console.WriteLine("8. Get Subcategories");
    Console.WriteLine("9. Get Top-Level Categories");
    Console.WriteLine("10. Get Course Roster");

    Console.WriteLine("\n--- Update Operations ---");
    Console.WriteLine("11. Update Course Price");
    Console.WriteLine("12. Update Enrollment Progress");
    Console.WriteLine("13. Publish Course");

    Console.WriteLine("\n--- Delete Operations ---");
    Console.WriteLine("14. Unenroll Student");
    Console.WriteLine("15. Delete Category");
    Console.WriteLine("16. Delete Course With Enrollments");

    Console.WriteLine("\n0. Exit");
    Console.Write("\nChoose an operation: ");

    if (!int.TryParse(Console.ReadLine(), out int choice))
    {
        Console.WriteLine("Invalid choice. Try again.");
        continue;
    }

    switch (choice)
    {
        case 1:
            operations.AddInstructor();
            break;

        case 2:
            operations.AddCourse();
            break;

        case 3:
            operations.AddStudents();
            break;

        case 4:
            operations.EnrollStudent();
            break;

        case 5:
            operations.FindCourseById();
            break;

        case 6:
            operations.FindCourseByTitle();
            break;

        case 7:
            operations.FilterCoursesByPrice();
            break;

        case 8:
            operations.GetSubCategories();
            break;

        case 9:
            operations.GetTopLevelCategories();
            break;

        case 10:
            operations.GetCourseRoster();
            break;

        case 11:
            operations.UpdateCoursePrice();
            break;

        case 12:
            operations.UpdateEnrollmentProgress();
            break;

        case 13:
            operations.PublishCourse();
            break;

        case 14:
            operations.UnenrollStudent();
            break;

        case 15:
            operations.DeleteCategory();
            break;

        case 16:
            operations.DeleteCourseWithEnrollments();
            break;

        case 0:
            Console.WriteLine("Goodbye!");
            return;

        default:
            Console.WriteLine("Invalid choice. Try again.");
            break;
    }
}
