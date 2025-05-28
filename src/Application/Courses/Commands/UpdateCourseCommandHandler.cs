using MediatR;
using Application.Common.Models;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Request.Course;
using Application.Common.Reponses;
using Infrastructure.Services;
using Domain.Common.Interfaces;
using System.Text;
using Microsoft.Extensions.Configuration;
namespace Application.Courses.Commands;


public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseR, SingleResponse>
{
    private readonly ApplicationDbContext _context;
    private readonly IFileStorageService _fileStorageService;
    private readonly IConfiguration _configuration;
    public UpdateCourseCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(UpdateCourseR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();

        var course = await _context.Courses.FindAsync(new object[] { request.Id }, cancellationToken);

        if (course == null)
            return res.SetError("Course not found");


        var endpoint = _configuration.GetSection("MinIO:Endpoint").Value;
        var bucketName = _configuration.GetSection("MinIO:BucketName").Value;
        StringBuilder imageUrl = new StringBuilder($"{endpoint}/{bucketName}/");
        Media? media = null;
        if (request.Image != null)
        {
            using var stream = request.Image.OpenReadStream();
            var url = await _fileStorageService.UploadFileAsync(stream, request.Image.FileName, request.Image.ContentType);
            imageUrl.Append(url);

            media = Media.Create(
                request.Image.FileName,
                request.Image.ContentType,
                imageUrl.ToString(),
                course.Id,
                "Course"
            );
        }
        var title = request.Title ?? course.Title;
        var description = request.Description ?? course.Description;
        var price = request.Price ?? course.Price;
        var isPublished = request.IsPublished ?? course.IsPublished;
        var level = Enum.TryParse(request.Level, out CourseLevel levelEnum) ? levelEnum : CourseLevel.Beginner;
        var status = request.Status ?? course.Status;
        var category = request.Category ?? course.Category;
        var tags = request.Tags ?? course.Tags;
        var prerequisites = request.Prerequisites ?? course.Prerequisites;
        var rating = request.Rating ?? course.Rating;
        var totalEnrollments = request.TotalEnrollments ?? course.TotalEnrollments;
        var syllabus = request.Syllabus ?? course.Syllabus;
        var learningObjectives = request.LearningObjectives ?? course.LearningObjectives;
        var requirements = request.Requirements ?? course.Requirements;
        var targetAudience = request.TargetAudience ?? course.TargetAudience;
        course.Update(title, description, price, isPublished, imageUrl.ToString(), status, level, category, tags, prerequisites, rating, totalEnrollments, syllabus, learningObjectives, requirements, targetAudience);

        await _context.SaveChangesAsync(cancellationToken);

        return res.SetSuccess(course.ToViewDto());


    }
}