using System.Text.Json.Serialization;
using Domain.Entities;

namespace Domain.Entities;


partial class Course
{
    #region -- Methods --

    /// <summary>
    /// Initialize
    /// </summary>
    public Course()
    {

    }
    
    public static Course Create(Guid id,Guid instructorId, string title, string description, decimal price, bool isPublished, string? imageUrl = null, int status = 0, CourseLevel level = CourseLevel.Beginner, string category = "", List<string>? tags = null, List<Guid>? prerequisites = null, decimal rating = 0, int totalEnrollments = 0, string syllabus = "", string learningObjectives = "", string requirements = "", string targetAudience = "")
    {
        var res = new Course
        {
            Id = id,
            Title = title,
            Description = description,
            Price = price,
            InstructorId = instructorId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Status = status,
            IsPublished = isPublished,
            IsDeleted = false,
            ImageUrl = imageUrl,
            Level = level,
            Category = category,
            Tags = tags ?? new List<string>(),
            Prerequisites = prerequisites ?? new List<Guid>(),
            Rating = rating,
            TotalEnrollments = totalEnrollments,
            Syllabus = syllabus,
            LearningObjectives = learningObjectives,
            Requirements = requirements,
            TargetAudience = targetAudience
        };

        return res;
    }


    public void Update(string title, string description, decimal price, bool isPublished, string? imageUrl = null, int status = 0, CourseLevel level = CourseLevel.Beginner, string category = "", List<string>? tags = null, List<Guid>? prerequisites = null, decimal rating = 0, int totalEnrollments = 0, string syllabus = "", string learningObjectives = "", string requirements = "", string targetAudience = "")
    {
        Title = title;
        Description = description;
        Price = price;
        IsPublished = isPublished;
        UpdatedAt = DateTime.UtcNow;
        Status = status;
        IsPublished = isPublished;
        IsDeleted = false;
        ImageUrl = imageUrl;
        Level = level;
        Category = category;
        Tags = tags ?? new List<string>();
        Prerequisites = prerequisites ?? new List<Guid>();
        Rating = rating;
        TotalEnrollments = totalEnrollments;
        Syllabus = syllabus;
        LearningObjectives = learningObjectives;
        Requirements = requirements;
        TargetAudience = targetAudience;
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="hide">Hide option</param>
    /// <param name="isArchived">IsArchived</param>
    /// <param name="modifiedBy">Modified by</param>
    public void UpdateVisibility()
    {
        IsPublished = !IsPublished;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="modifiedBy">Modified by</param>
    public void Delete()
    {
    }

    /// <summary>
    /// Convert to data transfer object
    /// </summary>
    /// <returns>Return the DTO</returns>
    public SearchDto ToSearchDto()
    {
        return new SearchDto
        {
            Id = Id
        };
    }

    /// <summary>
    /// Convert to data transfer object
    /// </summary>
    /// <returns>Return the DTO</returns>
    public ViewDto ToViewDto()
    {
        var res = ToBaseDto<ViewDto>();

        return res;
    }

    /// <summary>
    /// Convert to data transfer object
    /// </summary>
    /// <returns>Return the DTO</returns>
    public T ToBaseDto<T>() where T : BaseDto, new()
    {
        return new T
        {
            Id = Id,
            Title = Title,
            Description = Description,
            Price = Price,
            IsPublished = IsPublished,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt ?? DateTime.UtcNow,
            IsDeleted = IsDeleted,
            ImageUrl = ImageUrl,
            Duration = Duration,
            Level = Level,
            Category = Category,
            Tags = Tags,
            Prerequisites = Prerequisites,
            Rating = Rating,
            TotalEnrollments = TotalEnrollments,
            Syllabus = Syllabus,
            LearningObjectives = LearningObjectives,
            Requirements = Requirements,
            TargetAudience = TargetAudience
        };
    }

    #endregion

    #region -- Classes --

    /// <summary>
    /// Base
    /// </summary>
    public class BaseDto
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? ImageUrl { get; set; }
        public int Duration { get; set; }
        public CourseLevel Level { get; set; }
        public string Category { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public List<Guid> Prerequisites { get; set; } = new List<Guid>();
        public decimal Rating { get; set; }
        public int TotalEnrollments { get; set; }
        public string? Syllabus { get; set; }
        public string? LearningObjectives { get; set; }
        public string? Requirements { get; set; }
        public string? TargetAudience { get; set; }

    }

    /// <summary>
    /// Search
    /// </summary>
    public class SearchDto : BaseDto
    {

    }

    /// <summary>
    /// View
    /// </summary>
    public class ViewDto : BaseDto
    {

    }

    #endregion
}
