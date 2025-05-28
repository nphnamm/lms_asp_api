using System;
using Domain.Entities;

namespace Domain.Entities;

partial class VisitorAnalytics
{
    #region -- Methods --

    /// <summary>
    /// Initialize
    /// </summary>
    public VisitorAnalytics()
    {
    }

    public static VisitorAnalytics Create(
        string ipAddress,
        string userAgent,
        string? referrerUrl,
        string? pageUrl,
        string? country,
        string? city,
        string? deviceType,
        string? browser,
        string? operatingSystem,
        Guid? userId,
        Guid? courseId,
        Guid? lessonId,
        bool isUniqueVisit
    )
    {
        var res = new VisitorAnalytics
        {
            Id = Guid.NewGuid(),
            VisitDate = DateTime.UtcNow,
            IpAddress = ipAddress,
            UserAgent = userAgent,
            ReferrerUrl = referrerUrl,
            PageUrl = pageUrl,
            Country = country,
            City = city,
            DeviceType = deviceType,
            Browser = browser,
            OperatingSystem = operatingSystem,
            UserId = userId,
            CourseId = courseId,
            LessonId = lessonId,
            TimeSpent = 0,
            IsUniqueVisit = isUniqueVisit,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return res;
    }

    public void UpdateTimeSpent(int timeSpent)
    {
        TimeSpent = timeSpent;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateLocation(string country, string city)
    {
        Country = country;
        City = city;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDeviceInfo(string deviceType, string browser, string operatingSystem)
    {
        DeviceType = deviceType;
        Browser = browser;
        OperatingSystem = operatingSystem;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        // Implement soft delete if needed
    }

    #endregion
} 