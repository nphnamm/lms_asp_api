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
        
        // Set bucket policy to allow public read access
        await SetBucketPolicyAsync();
    }

    private async Task SetBucketPolicyAsync()
    {
        try
        {
            // Create a JSON policy that allows public read access for MinIO Community
            var policyJson = $@"{{
                ""Version"": ""2012-10-17"",
                ""Statement"": [
                    {{
                        ""Effect"": ""Allow"",
                        ""Principal"": {{
                            ""AWS"": ""*""
                        }},
                        ""Action"": ""s3:GetObject"",
                        ""Resource"": ""arn:aws:s3:::{_bucketName}/*""
                    }},
                    {{
                        ""Effect"": ""Allow"",
                        ""Principal"": ""*"",
                        ""Action"": ""s3:GetObject"",
                        ""Resource"": ""arn:aws:s3:::{_bucketName}/*""
                    }}
                ]
            }}";
            
            await _minioClient.SetPolicyAsync(new SetPolicyArgs().WithBucket(_bucketName).WithPolicy(policyJson));
            Console.WriteLine($"Successfully set public read policy for bucket: {_bucketName}");
        }
        catch (Exception ex)
        {
            // Log the error but don't fail the service initialization
            Console.WriteLine($"Warning: Could not set bucket policy for {_bucketName}: {ex.Message}");
            Console.WriteLine("You may need to set the bucket policy manually via MinIO Console");
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

        // Ensure bucket policy is set after upload
        await EnsureBucketIsPublicAsync();

        return objectName;
    }

    private async Task EnsureBucketIsPublicAsync()
    {
        try
        {
            // Check if bucket policy exists, if not set it
            await SetBucketPolicyAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Warning: Could not ensure bucket is public: {ex.Message}");
        }
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

    public string GetDirectMinioUrl(string fileName)
    {
        var endpoint = _minioClient.Config.Endpoint;
        var protocol = _minioClient.Config.Secure ? "https" : "http";
        return $"{protocol}://{endpoint}/{_bucketName}/{fileName}";
    }

    public async Task<string> GetPresignedUrlAsync(string fileName, int expiry = 3600)
    {
        try
        {
            var presignedUrl = await _minioClient.PresignedGetObjectAsync(
                new PresignedGetObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(fileName)
                    .WithExpiry(expiry));
            
            return presignedUrl;
        }
        catch (Exception)
        {
            // Fallback to API endpoint if presigned URL generation fails
            return GetFileUrl(fileName);
        }
    }
} 