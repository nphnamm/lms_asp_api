using System;
using System.IO;
using System.Threading.Tasks;
using Domain.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;

namespace Infrastructure.Services;

public class MinioStorageService : IFileStorageService
{
    private readonly IMinioClient _minioClient;
    private readonly string _bucketName;

    public MinioStorageService(IConfiguration configuration)
    {
        var endpoint = configuration["MinIO:Endpoint"] ?? throw new ArgumentNullException("MinIO:Endpoint");
        var accessKey = configuration["MinIO:AccessKey"] ?? throw new ArgumentNullException("MinIO:AccessKey");
        var secretKey = configuration["MinIO:SecretKey"] ?? throw new ArgumentNullException("MinIO:SecretKey");
        _bucketName = configuration["MinIO:BucketName"] ?? throw new ArgumentNullException("MinIO:BucketName");
        var useSSL = bool.Parse(configuration["MinIO:UseSSL"] ?? "false");

        _minioClient = new MinioClient()
            .WithEndpoint(endpoint)
            .WithCredentials(accessKey, secretKey)
            .WithSSL(useSSL)
            .Build();

        InitializeBucketAsync().Wait();
    }

    private async Task InitializeBucketAsync()
    {
        var bucketExists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName));
        if (!bucketExists)
        {
            await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName));
        }
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
    {
        var sanitizedFileName = fileName.Replace(" ", "");
        var objectName = $"{Guid.NewGuid()}_{sanitizedFileName}";
        
        await _minioClient.PutObjectAsync(
            new PutObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(objectName)
                .WithStreamData(fileStream)
                .WithObjectSize(fileStream.Length)
                .WithContentType(contentType));

        return objectName;
    }

    public async Task<Stream> DownloadFileAsync(string fileName)
    {
        var memoryStream = new MemoryStream();
        
        await _minioClient.GetObjectAsync(
            new GetObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(fileName)
                .WithCallbackStream(stream => stream.CopyTo(memoryStream)));

        memoryStream.Position = 0;
        return memoryStream;
    }

    public async Task DeleteFileAsync(string fileName)
    {
        await _minioClient.RemoveObjectAsync(
            new RemoveObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(fileName));
    }

    public string GetFileUrl(string fileName)
    {
        return $"/api/files/{fileName}";
    }
} 