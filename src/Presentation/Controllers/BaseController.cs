using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected User? CurrentUser => HttpContext.Items["CurrentUser"] as User;

    protected Guid? CurrentUserId => CurrentUser?.Id;

    protected bool IsInstructor => User.IsInRole("Instructor");

    protected bool IsAdmin => User.IsInRole("Admin");

    protected bool IsStudent => User.IsInRole("Student");
} 