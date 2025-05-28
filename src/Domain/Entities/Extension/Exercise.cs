using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Enums;
namespace Domain.Entities;


partial class Exercise
{
    #region -- Methods --

    /// <summary>
    /// Initialize
    /// </summary>
    public Exercise()
    {

    }

    public static Exercise Create(Guid courseId, string title, string content, int order, bool isPublished, ExerciseType type, int status, int timeLimit, decimal passingScore, int retryLimit, bool allowPartialCredit, string feedback, string instructions, decimal weight, bool isGraded, bool showAnswers, DateTime dueDate, string hints, decimal averageScore, int attemptCount)
    {
        var res = new Exercise
        {
            Id = Guid.NewGuid(),
            Title = title,
            Content = content,
            Order = order,
            Type = type,
            IsPublished = isPublished,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Status = status,
            TimeLimit = timeLimit,
            PassingScore = passingScore,
            RetryLimit = retryLimit,
            AllowPartialCredit = allowPartialCredit,
            Feedback = feedback,
            Instructions = instructions,
            Weight = weight,
            IsGraded = isGraded,
            ShowAnswers = showAnswers,
            DueDate = dueDate,
            Hints = hints,
            AverageScore = averageScore,
            AttemptCount = attemptCount
        };

        return res;
    }


    public void Update(string title, string content, int order, bool isPublished, ExerciseType type, int status, int timeLimit, decimal passingScore, int retryLimit, bool allowPartialCredit, string feedback, string instructions, decimal weight, bool isGraded, bool showAnswers, DateTime dueDate, string hints, decimal averageScore, int attemptCount)
    {
        Title = title;
        Content = content;
        Order = order;
        IsPublished = isPublished;
        UpdatedAt = DateTime.UtcNow;
        Type = type;
        Status = status;
        TimeLimit = timeLimit;
        PassingScore = passingScore;
        RetryLimit = retryLimit;
        AllowPartialCredit = allowPartialCredit;
        Feedback = feedback;
        Instructions = instructions;
        Weight = weight;
        IsGraded = isGraded;
        ShowAnswers = showAnswers;
        DueDate = dueDate;
        Hints = hints;
        AverageScore = averageScore;
        AttemptCount = attemptCount;

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
            Status = Status,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt ?? DateTime.UtcNow,
            TimeLimit = TimeLimit,
            PassingScore = PassingScore,
            RetryLimit = RetryLimit,
            AllowPartialCredit = AllowPartialCredit,
            Feedback = Feedback,
            Instructions = Instructions,
            Weight = Weight,
            IsGraded = IsGraded,
            ShowAnswers = ShowAnswers,
            DueDate = DueDate,
            Hints = Hints,
            AverageScore = AverageScore,
            AttemptCount = AttemptCount
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
        public ExerciseType Type { get; set; }
        public int Status { get; set; }
        public bool IsPublished { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int TimeLimit { get; set; }
        public decimal PassingScore { get; set; }
        public int RetryLimit { get; set; }
        public bool AllowPartialCredit { get; set; }
        public string? Feedback { get; set; }
        public string? Instructions { get; set; }
        public decimal Weight { get; set; }
        public bool IsGraded { get; set; }
        public bool ShowAnswers { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Hints { get; set; }
        public decimal AverageScore { get; set; }
        public int AttemptCount { get; set; }
        
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
