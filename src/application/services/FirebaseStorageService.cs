using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Storage.v1;
using Google.Apis.Storage.v1.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Chefio.Application.Interfaces.Services;

public class FirebaseStorageService : IFirebaseStorageService
{
    private readonly string _bucket;
    private readonly string _serviceAccountJson;

    public FirebaseStorageService(IConfiguration configuration)
    {
        _bucket = configuration["Firebase:Bucket"];
        var serviceAccountSection = configuration.GetSection("Firebase:ServiceAccount");
        _serviceAccountJson = System.Text.Json.JsonSerializer.Serialize(serviceAccountSection.GetChildren().ToDictionary(x => x.Key, x => x.Value));
    }

    public async Task<string> UploadFileAsync(IFormFile file, string subfolder)
    {
        var credential = GoogleCredential.FromJson(_serviceAccountJson);
        var storageService = new StorageService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = "Chefio"
        });

        var extension = Path.GetExtension(file.FileName);
        var fileName = $"chefio_{subfolder}_{Guid.NewGuid():N}{extension}";
        var objectName = $"chefio/{subfolder}/{fileName}";

        using var stream = file.OpenReadStream();
        var uploadRequest = storageService.Objects.Insert(
            new Google.Apis.Storage.v1.Data.Object() { Bucket = _bucket, Name = objectName },
            _bucket,
            stream,
            file.ContentType
        );
        await uploadRequest.UploadAsync();

        return $"https://storage.googleapis.com/{_bucket}/{objectName}";
    }

    public async Task DeleteFileAsync(string fileName)
    {
        var credential = GoogleCredential.FromJson(_serviceAccountJson);
        var storage = Google.Cloud.Storage.V1.StorageClient.Create(credential);
        await storage.DeleteObjectAsync(_bucket, fileName);
    }

    public string GetSignedUrl(string fileName, int expiresInSeconds)
    {
        var credential = GoogleCredential.FromJson(_serviceAccountJson);
        using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(_serviceAccountJson));
        var urlSigner = Google.Cloud.Storage.V1.UrlSigner.FromServiceAccountData(stream);
        return urlSigner.Sign(_bucket, fileName, TimeSpan.FromSeconds(expiresInSeconds));
    }
    
    public async Task<string> UploadFileAsync(IFormFile file)
    {
        return await UploadFileAsync(file, "uploads");
    }
}