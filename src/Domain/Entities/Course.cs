

namespace Domain.Entities;

public partial class Course
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Guid InstructorId { get; set; }
    public User Instructor { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsPublished { get; set; }
    public ICollection<Lesson> Lessons { get; set; }
    public ICollection<Enrollment> Enrollments { get; set; }
}