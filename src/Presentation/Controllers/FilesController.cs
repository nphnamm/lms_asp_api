using System;
using System.IO;
using System.Threading.Tasks;
using Domain.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly IFileStorageService _fileStorageService;

    public FilesController(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    [HttpPost("upload")]
    [Authorize]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded");

        using var stream = file.OpenReadStream();
        var fileName = await _fileStorageService.UploadFileAsync(stream, file.FileName, file.ContentType);
        
        return Ok(new { FileName = fileName, Url = _fileStorageService.GetFileUrl(fileName) });
    }

    [HttpGet("{fileName}")]
    public async Task<IActionResult> DownloadFile(string fileName)
    {
        try
        {
            var stream = await _fileStorageService.DownloadFileAsync(fileName);
            return File(stream, "application/octet-stream", fileName);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [HttpDelete("{fileName}")]
    [Authorize]
    public async Task<IActionResult> DeleteFile(string fileName)
    {
        try
        {
            await _fileStorageService.DeleteFileAsync(fileName);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
} 