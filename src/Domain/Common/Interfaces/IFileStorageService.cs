using System.Threading.Tasks;

namespace Domain.Common.Interfaces;

public interface IFileStorageService
{
    Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType);
    Task<Stream> DownloadFileAsync(string fileName);
    Task DeleteFileAsync(string fileName);
    string GetFileUrl(string fileName);
    string GetDirectMinioUrl(string fileName);
    Task<string> GetPresignedUrlAsync(string fileName, int expiry = 3600);
} 