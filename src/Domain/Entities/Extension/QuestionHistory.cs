namespace Domain.Entities;


partial class QuestionHistory   
{
    #region -- Methods --

    /// <summary>
    /// Initialize
    /// </summary>
    public QuestionHistory()
    {

    }
    

    public static QuestionHistory Create(Guid questionId, Guid exerciseHistoryId, bool isCorrect) 
    {
        var res = new QuestionHistory
        {
            Id = Guid.NewGuid(),
            QuestionId = questionId,
            ExerciseHistoryId = exerciseHistoryId,
            AnsweredAt = DateTime.UtcNow,
            IsCorrect = isCorrect,
            Status = 0,
            IsDeleted = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,

        };

        return res;
    }


    public void Update(bool isCorrect, int status)
    {
        IsCorrect = isCorrect;
        Status = status;
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
            QuestionId = QuestionId,
            ExerciseHistoryId = ExerciseHistoryId,
            AnsweredAt = AnsweredAt,
            IsCorrect = IsCorrect,
            Status = Status,
            IsDeleted = IsDeleted,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt
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
        public Guid QuestionId { get; set; }
        public Guid ExerciseHistoryId { get; set; }
        public DateTime AnsweredAt { get; set; }
        public bool IsCorrect { get; set; }
        public int Status { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
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
