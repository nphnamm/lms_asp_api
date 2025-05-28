using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Enums;
namespace Domain.Entities;


partial class Lesson
{
    #region -- Methods --

    /// <summary>
    /// Initialize
    /// </summary>
    public Lesson()
    {

    }

    public static Lesson Create(Guid courseId, string title, string description, string content, int order, bool isPublished, int status, LessonType type, int duration, string videoUrl, string resources, List<string> keywords, decimal completionRate, int viewCount, string notes, bool isPreview)
    {
        var res = new Lesson
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            Content = content,
            Order = order,
            Status = status,
            IsPublished = isPublished,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false,
            CourseId = courseId,
            Type = type,
            Duration = duration,
            VideoUrl = videoUrl,
            Resources = resources,
            Keywords = keywords,
            CompletionRate = completionRate,
            ViewCount = viewCount,
            Notes = notes,
            IsPreview = isPreview
            
        };

        return res;
    }


    public void Update(string title, string content, int order, bool isPublished, int status, LessonType type, int duration, string videoUrl, string resources, List<string> keywords, decimal completionRate, int viewCount, string notes, bool isPreview)
    {
        Title = title;
        Content = content;
        Order = order;
        IsPublished = isPublished;
        Status = status;
        Type = type;
        Duration = duration;
        VideoUrl = videoUrl;
        Resources = resources;
        Keywords = keywords;
        CompletionRate = completionRate;
        ViewCount = viewCount;
        Notes = notes;
        IsPreview = isPreview;
        UpdatedAt = DateTime.UtcNow;
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
        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;
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
            Content = Content,
            Order = Order,
            Status = Status,
            IsPublished = IsPublished,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt ?? DateTime.UtcNow,
            Type = Type,
            Duration = Duration,
            VideoUrl = VideoUrl,
            Resources = Resources,
            Keywords = Keywords,
            CompletionRate = CompletionRate,
            ViewCount = ViewCount,
            Notes = Notes,
            IsPreview = IsPreview
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
        public string Content { get; set; }
        public int Order { get; set; }
        public int Status { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int Duration { get; set; }
        public string? VideoUrl { get; set; }
        public string? Resources { get; set; }
        public List<string> Keywords { get; set; } = new List<string>();
        public decimal CompletionRate { get; set; }
        public int ViewCount { get; set; }
        public string? Notes { get; set; }
        public bool IsPreview { get; set; }

        public LessonType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
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
