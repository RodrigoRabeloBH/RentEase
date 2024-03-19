using Microsoft.AspNetCore.Http;
using RentEase.Domain.Entities;

namespace RentEase.Domain.Interfaces.Services
{
    public interface ICloudinaryServices
    {
        Task<List<Image>> UploadImages(List<IFormFile> files);
        Task DeleteImages(List<Image> images);
    }
}
