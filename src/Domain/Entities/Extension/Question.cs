
using Domain.Enums;


namespace Domain.Entities;


partial class Question
{
    #region -- Methods --

    /// <summary>
    /// Initialize
    /// </summary>
    public Question()
    {

    }

    public static Question Create(Guid exerciseId, Guid lessonId, string text, ExerciseType type, int order)
    {
        var res = new Question
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            ExerciseId = exerciseId,
            LessonId = lessonId,
            Text = text,
            Type = type,
            Order = order
        };

        return res;
    }


    public void Update(string text, ExerciseType type, int order)
    {
        Text = text;
        Type = type;
        Order = order;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="hide">Hide option</param>
    /// <param name="isArchived">IsArchived</param>
    /// <param name="modifiedBy">Modified by</param>
    // public void UpdateVisibility()
    // {
    //     IsPublished = !IsPublished;
    //     UpdatedAt = DateTime.UtcNow;
    // }

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
            Text = Text,
            Type = Type,
            Options = Options.Select(o => o.ToBaseDto<Option.BaseDto>()).ToList(),
            Order = Order,
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
        public string Text { get; set; }
        public ExerciseType Type { get; set; }
        public List<Option.BaseDto> Options { get; set; }
        public int Order { get; set; }
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
