namespace Domain.Entities;


partial class ExerciseHistory
{
    #region -- Methods --

    /// <summary>
    /// Initialize
    /// </summary>
    public ExerciseHistory()
    {

    }


    public static ExerciseHistory Create(Guid exerciseId, Guid userId, DateTime startedAt, DateTime completedAt, decimal score)
    {
        var res = new ExerciseHistory
        {
            Id = Guid.NewGuid(),
            ExerciseId = exerciseId,
            UserId = userId,
            StartedAt = startedAt,
            CompletedAt = completedAt,
            Score = score,
            TimeTaken = (int)(completedAt - startedAt).TotalMinutes,
            Status = 1,
            IsDeleted = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,

        };

        return res;
    }


    public void Update(decimal score, int status, bool isDeleted)
    {
        Score = score;
        Status = status;
        IsDeleted = isDeleted;
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
            ExerciseId = ExerciseId,
            UserId = UserId,
            StartedAt = StartedAt,
            CompletedAt = CompletedAt,
            Score = Score,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt,
            IsDeleted = IsDeleted,
            QuestionHistories = QuestionHistories.Select(q => q.ToBaseDto<QuestionHistory.BaseDto>()).ToList()

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
        public Guid ExerciseId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public decimal? Score { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<QuestionHistory.BaseDto> QuestionHistories { get; set; } = new List<QuestionHistory.BaseDto>();
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
    public ICollection<QuestionHistory> QuestionHistories { get; set; } = new List<QuestionHistory>();

}
