

using System.Text.Json.Serialization;
using Domain.Entities;

namespace Domain.Entities;


partial class CourseProgress
{
    #region -- Methods --

    /// <summary>
    /// Initialize
    /// </summary>
    public CourseProgress()
    {

    }
    
    public static CourseProgress Create(Guid id,Guid courseId, Guid userId, DateTime learningDate, CourseProgressStatus status, bool isCompleted)
    {
        var res = new CourseProgress
        {
            Id = id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CourseId = courseId,
            UserId = userId,
            LearningDate = learningDate,
            Status = status,
            IsCompleted = isCompleted,
            IsDeleted = false
        };

        return res;
    }


    public void Update(CourseProgressStatus status, bool isCompleted)
    {
        Status = status;
        IsCompleted = isCompleted;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="hide">Hide option</param>
    /// <param name="isArchived">IsArchived</param>
    /// <param name="modifiedBy">Modified by</param>

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
            CourseId = CourseId,
            UserId = UserId,
            LearningDate = LearningDate,
            Status = Status,
            IsCompleted = IsCompleted,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt ,
            IsDeleted = IsDeleted
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
        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }
        public DateTime LearningDate { get; set; }
        public CourseProgressStatus Status { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsCompleted { get; set; } = false;
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
