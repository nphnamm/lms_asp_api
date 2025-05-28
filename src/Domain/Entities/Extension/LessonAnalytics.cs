using System;
using Domain.Entities;

namespace Domain.Entities;

partial class LessonAnalytics
{
    #region -- Methods --

    /// <summary>
    /// Initialize
    /// </summary>
    public LessonAnalytics()
    {
    }

    public static LessonAnalytics Create(Guid lessonId)
    {
        var res = new LessonAnalytics
        {
            Id = Guid.NewGuid(),
            LessonId = lessonId,
            Date = DateTime.UtcNow.Date,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return res;
    }

    public void UpdateViewMetrics(int totalViews, int uniqueViews, int previewViews, int enrolledViews)
    {
        TotalViews = totalViews;
        UniqueViews = uniqueViews;
        PreviewViews = previewViews;
        EnrolledViews = enrolledViews;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateEngagementMetrics(int totalTimeSpent, decimal averageTimeSpent, int totalInteractions, decimal completionRate)
    {
        TotalTimeSpent = totalTimeSpent;
        AverageTimeSpent = averageTimeSpent;
        TotalInteractions = totalInteractions;
        CompletionRate = completionRate;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateExerciseMetrics(int totalAttempts, int successfulAttempts, decimal averageScore, int averageAttempts)
    {
        TotalExerciseAttempts = totalAttempts;
        SuccessfulExerciseAttempts = successfulAttempts;
        AverageExerciseScore = averageScore;
        AverageAttemptsPerExercise = averageAttempts;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDropOffMetrics(int dropOffCount, decimal dropOffRate, string? mostCommonDropOffPoint)
    {
        DropOffCount = dropOffCount;
        DropOffRate = dropOffRate;
        MostCommonDropOffPoint = mostCommonDropOffPoint;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateFeedbackMetrics(int helpRequests, int reportedIssues, decimal userSatisfaction)
    {
        HelpRequests = helpRequests;
        ReportedIssues = reportedIssues;
        UserSatisfaction = userSatisfaction;
        UpdatedAt = DateTime.UtcNow;
    }

    public void IncrementViews(bool isUnique = false, bool isPreview = false, bool isEnrolled = false)
    {
        TotalViews++;
        if (isUnique) UniqueViews++;
        if (isPreview) PreviewViews++;
        if (isEnrolled) EnrolledViews++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddExerciseAttempt(bool isSuccessful, decimal score)
    {
        TotalExerciseAttempts++;
        if (isSuccessful) SuccessfulExerciseAttempts++;
        
        // Recalculate average score
        AverageExerciseScore = ((AverageExerciseScore * (TotalExerciseAttempts - 1)) + score) / TotalExerciseAttempts;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddDropOff(string? dropOffPoint)
    {
        DropOffCount++;
        if (dropOffPoint != null)
        {
            MostCommonDropOffPoint = dropOffPoint;
        }
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddHelpRequest()
    {
        HelpRequests++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddReportedIssue()
    {
        ReportedIssues++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateUserSatisfaction(decimal satisfaction)
    {
        UserSatisfaction = satisfaction;
        UpdatedAt = DateTime.UtcNow;
    }

    #endregion
} 