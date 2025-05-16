using System;

namespace Domain.Entities;

public class CourseImage
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public string Url { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }

    public static CourseImage Create(Guid courseId, string fileName, string contentType, string url)
    {
        return new CourseImage
        {
            Id = Guid.NewGuid(),
            CourseId = courseId,
            FileName = fileName,
            ContentType = contentType,
            Url = url,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true
        };
    }

    public void Update(string fileName, string contentType, string url)
    {
        FileName = fileName;
        ContentType = contentType;
        Url = url;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
} 