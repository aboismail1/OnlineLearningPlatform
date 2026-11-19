namespace OnlineLearningPlatform.Entities;

public class Instructor
{
    public int InstructorId { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public string? Bio { get; set; }

    public DateTime JoinedDate { get; set; }

    public ICollection<Course> Courses { get; set; }
        = new List<Course>();
}