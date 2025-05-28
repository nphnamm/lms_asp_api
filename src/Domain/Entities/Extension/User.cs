
using Domain.Enums;


namespace Domain.Entities;


partial class User
{
    #region -- Methods --

    /// <summary>
    /// Initialize
    /// </summary>
    public User()
    {

    }

    public static User Create(Guid userId, string email, string phoneNumber, string userName)
    {
        var res = new User
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Email = email,
            PhoneNumber = phoneNumber,
            UserName = userName
        };

        return res;
    }

    public static User CreateUserDto(Guid userId, string email, string phoneNumber, string userName)
    {
        var res = new User
        {
            Id = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Email = email,
            PhoneNumber = phoneNumber,
            UserName = userName,
            IsActive = true,
            IsDeleted = false
        };

        return res;
    }


    public void Update(string email, string phoneNumber, string userName)
    {
        Email = email;
        PhoneNumber = phoneNumber;  
        UserName = userName;
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
            Email = Email,
            PhoneNumber = PhoneNumber,
            UserName = UserName,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt ?? DateTime.UtcNow,
            IsActive = IsActive,
            IsDeleted = IsDeleted,
            ProfilePicture = ProfilePicture,
            Bio = Bio,
            Education = Education,
            Qualifications = Qualifications,
            Skills = Skills,
            Preferences = Preferences,
            TimeZone = TimeZone,
            Language = Language,
            LastLoginAt = LastLoginAt,
            CompletedCourses = CompletedCourses,
            AverageScore = AverageScore,
            SocialLinks = SocialLinks,
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
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public string? ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public string? Education { get; set; }
        public string? Qualifications { get; set; }
        public string? Skills { get; set; }
        public string? Preferences { get; set; }
        public string? TimeZone { get; set; }
        public string? Language { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public int CompletedCourses { get; set; }
        public decimal AverageScore { get; set; }
        public string? SocialLinks { get; set; }
        public bool IsInstructor { get; set; }
        public string? InstructorBio { get; set; }
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
