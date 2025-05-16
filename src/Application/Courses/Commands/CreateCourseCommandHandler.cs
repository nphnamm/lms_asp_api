using MediatR;
using Application.Common.Models;
using Domain.Entities;
using Infrastructure.Data;
using Application.Request.Course;
using Application.Common.Reponses;
using Domain.Common.Interfaces;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Application.Courses.Commands;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseR, SingleResponse>
{
    private readonly ApplicationDbContext _context;
    private readonly IFileStorageService _fileStorageService;
    private readonly IConfiguration _configuration;
    
    public CreateCourseCommandHandler(
        ApplicationDbContext context, 
        IFileStorageService fileStorageService,
        IConfiguration configuration)
    {
        _context = context;
        _fileStorageService = fileStorageService;
        _configuration = configuration;
    }

    public async Task<SingleResponse> Handle(CreateCourseR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();
        var endpoint = _configuration.GetSection("MinIO:Endpoint").Value;
        var bucketName = _configuration.GetSection("MinIO:BucketName").Value;
        StringBuilder imageUrl = new StringBuilder($"{endpoint}/{bucketName}/");
        Media? media = null;
        var courseId = Guid.Empty;

        // Handle image upload if provided
        if (request.Image != null && request.Image.Length > 0)
        {
            using var stream = request.Image.OpenReadStream();
            var url = await _fileStorageService.UploadFileAsync(stream, request.Image.FileName, request.Image.ContentType);
            imageUrl.Append(url);
            
            media = Media.Create(
                request.Image.FileName,
                request.Image.ContentType,
                imageUrl.ToString(),
                courseId,
                "Course"
            );
        }

        var course = Course.Create(courseId, request.InstructorId, request.Title, request.Description, request.Price, true);
        course.ImageUrl = imageUrl.ToString();

        _context.Courses.Add(course);
        await _context.SaveChangesAsync(cancellationToken);

        // Update media with the correct course ID
        if (media != null)
        {
            media.EntityId = course.Id;
            _context.Media.Add(media);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return res.SetSuccess(course);
    }
}