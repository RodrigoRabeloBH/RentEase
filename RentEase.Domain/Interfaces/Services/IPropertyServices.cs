using Microsoft.AspNetCore.Http;
using RentEase.Domain.Dto;
using RentEase.Domain.Entities;

namespace RentEase.Domain.Interfaces.Services
{
    public interface IPropertyServices
    {
        Task<List<Property>> GetProperties();
        Task<Property> GetProperty(Guid id);
        Task<Property> Create(PropertyDto propertyDto);
        Task<bool> Update(Guid propertyId, PropertyDto propertyDto);
        Task<bool> Delete(Guid propertyId);
        Task UploadImages(List<IFormFile> files, Guid propertyId);
    }
}
