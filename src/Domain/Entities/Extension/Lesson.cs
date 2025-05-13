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

    public static Lesson Create(Guid courseId, string title, string content, int order, bool isPublished, LessonType type)
    {
        var res = new Lesson
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsPublished = isPublished,
            CourseId = courseId,
            Title = title,
            Content = content,
            Order = order,
            Type = type
        };

        return res;
    }


    public void Update(string title, string content, int order, bool isPublished)
    {
        Title = title;
        Content = content;
        Order = order;
        IsPublished = isPublished;
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
            Content = Content,
            Order = Order,
            Type = Type,
            IsPublished = IsPublished,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt ?? DateTime.UtcNow
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
        public string Content { get; set; }
        public int Order { get; set; }
        public LessonType Type { get; set; }
        public bool IsPublished { get; set; }
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
