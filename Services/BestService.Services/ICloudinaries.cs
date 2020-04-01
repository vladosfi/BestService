namespace BestService.Services
{
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;

    public interface ICloudinaries
    {
        Task<string> UploadImage(IFormFile fileForm, string name, Transformation transformation = null);
    }
}
