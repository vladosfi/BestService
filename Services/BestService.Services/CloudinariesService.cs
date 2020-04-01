namespace BestService.Services
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinariesService : ICloudinaries
    {
        private readonly Cloudinary cloudinary;

        public CloudinariesService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> UploadImage(
            IFormFile fileForm,
            string name,
            Transformation transformation = null)
        {
            fileForm = fileForm ?? throw new ArgumentNullException(nameof(fileForm));

            byte[] image;

            using (var memoryStream = new MemoryStream())
            {
                await fileForm.CopyToAsync(memoryStream);
                image = memoryStream.ToArray();
            }

            var imageStream = new MemoryStream(image);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(name, imageStream),
                Transformation = transformation,
            };

            var uploadResult = this.cloudinary.UploadAsync(uploadParams);

            imageStream.Dispose();
            return uploadResult.Result.SecureUri.AbsoluteUri;
        }
    }
}
