using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Chefio.Application.Interfaces.Services
{
    public interface IFirebaseStorageService
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task<string> UploadFileAsync(IFormFile file, string subfolder);
        Task DeleteFileAsync(string fileName);
        string GetSignedUrl(string fileName, int expiresInSeconds);
    }
}