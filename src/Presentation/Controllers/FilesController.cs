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
        
        return Ok(new { 
            FileName = fileName, 
            ApiUrl = _fileStorageService.GetFileUrl(fileName),
            DirectUrl = _fileStorageService.GetDirectMinioUrl(fileName)
        });
    }

    [HttpGet("{fileName}")]
    public async Task<IActionResult> DownloadFile(string fileName)
    {
        try
        {
            var stream = await _fileStorageService.DownloadFileAsync(fileName);
            return File(stream, "application/octet-stream", fileName);
        }
        catch (Exception ex)
        {
            // Log the specific error for debugging
            Console.WriteLine($"Error downloading file {fileName}: {ex.Message}");
            return NotFound($"File '{fileName}' not found or access denied");
        }
    }

    [HttpGet("{fileName}/presigned")]
    public async Task<IActionResult> GetPresignedUrl(string fileName, int expiry = 3600)
    {
        try
        {
            var presignedUrl = await _fileStorageService.GetPresignedUrlAsync(fileName, expiry);
            return Ok(new { PresignedUrl = presignedUrl, ExpiresIn = expiry });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating presigned URL for {fileName}: {ex.Message}");
            return NotFound($"File '{fileName}' not found or access denied");
        }
    }

    [HttpGet("{fileName}/direct")]
    public IActionResult GetDirectUrl(string fileName)
    {
        try
        {
            var directUrl = _fileStorageService.GetDirectMinioUrl(fileName);
            return Ok(new { DirectUrl = directUrl });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating direct URL for {fileName}: {ex.Message}");
            return NotFound($"File '{fileName}' not found");
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