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
    

    public static ExerciseHistory Create(Guid id,Guid exerciseId, Guid userId, DateTime startedAt, DateTime completedAt, decimal score)
    {
        var res = new ExerciseHistory
        {
            Id = id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            ExerciseId = exerciseId,
            UserId = userId,
            StartedAt = startedAt,
            CompletedAt = completedAt,
            Score = score,
            IsPassed = score >= 80
        };

        return res;
    }


    public void Update(DateTime completedAt, decimal score)
    {
        
        CompletedAt = completedAt;

        Score = score;
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
            IsPassed = IsPassed,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt,
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
        public Guid ExerciseId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public decimal? Score { get; set; }
        public bool IsPassed { get; set; }
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
