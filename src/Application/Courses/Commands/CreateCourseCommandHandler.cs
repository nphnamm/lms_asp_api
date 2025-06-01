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
        /*
        Mock Test Data Sets:

        1. Beginner ASP.NET Core Course:
        {
            "title": "Introduction to ASP.NET Core",
            "description": "Learn the fundamentals of building web applications with ASP.NET Core",
            "price": 49.99m,
            "instructorId": "00000000-0000-0000-0000-000000000001",
            "level": 0, // Beginner
            "category": 0, // CourseLevel.Beginner
            "tags": ["asp.net", "csharp", "web-development", "backend"],
            "prerequisites": [],
            "rating": 0,
            "totalEnrollments": 0,
            "syllabus": "1. Introduction to ASP.NET Core\n2. Setting up your development environment\n3. Understanding the project structure\n4. Controllers and Routing\n5. Models and Data Access\n6. Views and Razor Pages\n7. Authentication and Authorization\n8. API Development\n9. Testing\n10. Deployment",
            "learningObjectives": "By the end of this course, you will be able to:\n- Set up and configure an ASP.NET Core project\n- Create and manage controllers and routes\n- Implement data access using Entity Framework Core\n- Build RESTful APIs\n- Implement authentication and authorization\n- Deploy your application to production",
            "requirements": "- Basic knowledge of C#\n- Understanding of web development concepts\n- Visual Studio 2022 or VS Code\n- .NET 7.0 SDK or later",
            "targetAudience": "This course is designed for:\n- Beginner to intermediate C# developers\n- Web developers looking to learn ASP.NET Core\n- Students and professionals interested in backend development"
        }

        2. Advanced Machine Learning Course:
        {
            "title": "Advanced Machine Learning with Python",
            "description": "Master advanced machine learning concepts and implement complex algorithms using Python",
            "price": 99.99m,
            "instructorId": "00000000-0000-0000-0000-000000000002",
            "level": 2, // Advanced
            "category": 2, // CourseLevel.Advanced
            "tags": ["machine-learning", "python", "ai", "data-science", "deep-learning"],
            "prerequisites": ["00000000-0000-0000-0000-000000000001"], // Requires basic ML course
            "rating": 4.8m,
            "totalEnrollments": 1500,
            "syllabus": "1. Advanced Neural Networks\n2. Deep Learning Architectures\n3. Natural Language Processing\n4. Computer Vision\n5. Reinforcement Learning\n6. Generative Adversarial Networks\n7. Time Series Analysis\n8. Model Optimization\n9. Production Deployment\n10. Research Methods",
            "learningObjectives": "By the end of this course, you will be able to:\n- Implement complex neural network architectures\n- Build and train deep learning models\n- Apply advanced NLP and computer vision techniques\n- Develop reinforcement learning systems\n- Optimize and deploy ML models in production",
            "requirements": "- Strong Python programming skills\n- Understanding of basic machine learning concepts\n- Knowledge of linear algebra and calculus\n- GPU for deep learning (recommended)\n- Python 3.8+ and relevant ML libraries",
            "targetAudience": "This course is designed for:\n- Data scientists with intermediate ML experience\n- Software engineers transitioning to ML\n- Researchers and academics\n- AI/ML professionals seeking advanced knowledge"
        }

        3. Intermediate Web Development Course:
        {
            "title": "Full-Stack Web Development with React and Node.js",
            "description": "Build modern web applications using React for frontend and Node.js for backend",
            "price": 79.99m,
            "instructorId": "00000000-0000-0000-0000-000000000003",
            "level": 1, // Intermediate
            "category": 1, // CourseLevel.Intermediate
            "tags": ["react", "node.js", "javascript", "fullstack", "web-development"],
            "prerequisites": ["00000000-0000-0000-0000-000000000004"], // Requires basic web development
            "rating": 4.5m,
            "totalEnrollments": 2500,
            "syllabus": "1. Modern JavaScript Fundamentals\n2. React Core Concepts\n3. State Management with Redux\n4. Node.js and Express.js\n5. RESTful API Design\n6. Database Integration\n7. Authentication and Authorization\n8. Real-time Features with Socket.io\n9. Testing and Debugging\n10. Deployment and DevOps",
            "learningObjectives": "By the end of this course, you will be able to:\n- Build responsive React applications\n- Create RESTful APIs with Node.js\n- Implement state management solutions\n- Integrate databases and authentication\n- Deploy full-stack applications",
            "requirements": "- Basic HTML, CSS, and JavaScript knowledge\n- Understanding of web development concepts\n- Node.js and npm installed\n- Modern web browser\n- Code editor (VS Code recommended)",
            "targetAudience": "This course is designed for:\n- Frontend developers learning backend\n- Backend developers learning frontend\n- Web developers seeking full-stack skills\n- Computer science students"
        }

        4. Beginner Data Science Course:
        {
            "title": "Data Science Fundamentals with Python",
            "description": "Learn the basics of data science, statistics, and data analysis using Python",
            "price": 59.99m,
            "instructorId": "00000000-0000-0000-0000-000000000004",
            "level": 0, // Beginner
            "category": 0, // CourseLevel.Beginner
            "tags": ["data-science", "python", "statistics", "data-analysis", "pandas"],
            "prerequisites": [],
            "rating": 4.7m,
            "totalEnrollments": 3000,
            "syllabus": "1. Python for Data Science\n2. Data Analysis with Pandas\n3. Data Visualization\n4. Statistical Analysis\n5. Data Cleaning and Preprocessing\n6. Exploratory Data Analysis\n7. Basic Machine Learning\n8. Data Storytelling\n9. Data Ethics\n10. Capstone Project",
            "learningObjectives": "By the end of this course, you will be able to:\n- Perform data analysis using Python\n- Create effective data visualizations\n- Apply statistical methods to data\n- Clean and preprocess datasets\n- Build basic machine learning models",
            "requirements": "- Basic Python programming knowledge\n- High school level mathematics\n- Computer with Python 3.8+ installed\n- Jupyter Notebook\n- Basic understanding of data concepts",
            "targetAudience": "This course is designed for:\n- Beginners in data science\n- Business analysts\n- Students interested in data\n- Professionals seeking data skills"
        }
        */
        var res = new SingleResponse();
        var endpoint = _configuration.GetSection("MinIO:Endpoint").Value;
        var bucketName = _configuration.GetSection("MinIO:BucketName").Value;
        StringBuilder imageUrl = new StringBuilder($"{endpoint}/{bucketName}/");
        Media? media = null;
        var courseId = Guid.Empty;
        var url = "";


        // Handle image upload if provided
        if (request.Image != null && request.Image.Length > 0)
        {
            using var stream = request.Image.OpenReadStream();
            url = await _fileStorageService.UploadFileAsync(stream, request.Image.FileName, request.Image.ContentType);
            imageUrl.Append(url);
            
            media = Media.Create(
                request.Image.FileName,
                request.Image.ContentType,
                imageUrl.ToString(),
                courseId,
                "Course"
            );
        }

        var title = request.Title;
        var description = request.Description;
        var price = request.Price;
        var instructorId = request.InstructorId;
        var level = request.Level;
        var category = request.Category;
        var tags = request.Tags;
        var prerequisites = request.Prerequisites;
        var rating = request.Rating;
        var totalEnrollments = request.TotalEnrollments;
        var syllabus = request.Syllabus;
        var learningObjectives = request.LearningObjectives;
        var requirements = request.Requirements;
        var targetAudience = request.TargetAudience;


        var course = Course.Create(
            courseId,
            instructorId,
            title,
            description,
            price,
            true,
            imageUrl.ToString(),
            0, // status
            (CourseLevel)level,
            category.ToString(),
            tags,
            prerequisites,
            rating,
            totalEnrollments,
            syllabus,
            learningObjectives,
            requirements,
            targetAudience
        );

        _context.Courses.Add(course);
        await _context.SaveChangesAsync(cancellationToken);

        // Update media with the correct course ID
        if (media != null)
        {
            media.EntityId = course.Id;
            _context.Media.Add(media);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return res.SetSuccess(course.ToViewDto());
    }
}