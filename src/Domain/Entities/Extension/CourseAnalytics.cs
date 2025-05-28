using System;
using Domain.Entities;

namespace Domain.Entities;

partial class CourseAnalytics
{
    #region -- Methods --

    /// <summary>
    /// Initialize
    /// </summary>
    public CourseAnalytics()
    {
    }

    public static CourseAnalytics Create(Guid courseId)
    {
        var res = new CourseAnalytics
        {
            Id = Guid.NewGuid(),
            CourseId = courseId,
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

    public void UpdateEnrollmentMetrics(int newEnrollments, int completedEnrollments, int droppedEnrollments, decimal conversionRate)
    {
        NewEnrollments = newEnrollments;
        CompletedEnrollments = completedEnrollments;
        DroppedEnrollments = droppedEnrollments;
        ConversionRate = conversionRate;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateRevenueMetrics(decimal revenue, int refunds, decimal netRevenue)
    {
        Revenue = revenue;
        Refunds = refunds;
        NetRevenue = netRevenue;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdatePerformanceMetrics(decimal averageRating, int totalReviews, int positiveReviews, int negativeReviews)
    {
        AverageRating = averageRating;
        TotalReviews = totalReviews;
        PositiveReviews = positiveReviews;
        NegativeReviews = negativeReviews;
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

    public void IncrementEnrollments(bool isCompleted = false, bool isDropped = false)
    {
        NewEnrollments++;
        if (isCompleted) CompletedEnrollments++;
        if (isDropped) DroppedEnrollments++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddRevenue(decimal amount)
    {
        Revenue += amount;
        NetRevenue = Revenue - Refunds;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddRefund(decimal amount)
    {
        Refunds++;
        NetRevenue = Revenue - Refunds;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddReview(decimal rating, bool isPositive)
    {
        TotalReviews++;
        if (isPositive) PositiveReviews++;
        else NegativeReviews++;
        
        // Recalculate average rating
        AverageRating = ((AverageRating * (TotalReviews - 1)) + rating) / TotalReviews;
        UpdatedAt = DateTime.UtcNow;
    }

    #endregion
} 