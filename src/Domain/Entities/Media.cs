using System;

namespace Domain.Entities;

public partial class Media
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public string Url { get; set; }
    public string EntityType { get; set; }  // e.g., "Course", "Lesson", etc.
    public Guid EntityId { get; set; }     
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
} 