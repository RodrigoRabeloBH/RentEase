using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RentEase.Domain.Entities;
using RentEase.Domain.Interfaces.Services;

namespace RentEase.Application.Services
{
    public class CloudinaryServices : ICloudinaryServices
    {
        private readonly Cloudinary _cloudinary;
        private readonly IConfiguration _configuration;

        public CloudinaryServices(IConfiguration configuration)
        {
            _configuration = configuration;

            _cloudinary = new Cloudinary(_configuration["CLOUDINARY_URL"]);
            _cloudinary.Api.Secure = true;
        }

        public async Task<List<Image>> UploadImages(List<IFormFile> files)
        {
            var images = new List<Image>();

            if (files != null && files.Any())
            {
                foreach (var file in files)
                {
                    using var stream = file.OpenReadStream();

                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                    };

                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                    var image = new Image
                    {
                        PublicId = uploadResult.PublicId,
                        Uri = uploadResult.SecureUrl
                    };

                    images.Add(image);
                }
            }

            return images;
        }

        public async Task DeleteImages(List<Image> images)
        {
            if (images != null && images.Any())
            {
                foreach (var image in images)
                {
                    var deleteParams = new DeletionParams(image.PublicId);

                    var deletionResult = await _cloudinary.DestroyAsync(deleteParams);
                }
            }
        }
    }
}
