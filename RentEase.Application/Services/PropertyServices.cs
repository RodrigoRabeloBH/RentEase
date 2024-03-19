using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Http;
using RentEase.Contratcs;
using RentEase.Domain.Dto;
using RentEase.Domain.Entities;
using RentEase.Domain.Interfaces.Repositories;
using RentEase.Domain.Interfaces.Services;

namespace RentEase.Application.Services
{
    public class PropertyServices : IPropertyServices
    {
        private readonly IPropertyRepository _rep;
        private readonly IMapper _mapper;
        private readonly ICloudinaryServices _cloudinaryServices;
        private readonly IPublishEndpoint _publishEndpoint;

        public PropertyServices(IPropertyRepository rep,
            IMapper mapper,
            ICloudinaryServices cloudinaryServices,
            IPublishEndpoint publishEndpoint)
        {
            _rep = rep;
            _mapper = mapper;
            _cloudinaryServices = cloudinaryServices;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<List<Property>> GetProperties()
        {
            var properties = await _rep.GetPropertiesAsync();

            return properties;
        }

        public async Task<Property> GetProperty(Guid id)
        {
            var property = await _rep.GetPropertyAsync(id);

            return property;
        }

        public async Task<Property> Create(PropertyDto propertyDto)
        {
            var images = await _cloudinaryServices.UploadImages(propertyDto.Images);

            var property = _mapper.Map<Property>(propertyDto);

            property.Images = images;

            var created = await _rep.CreateAsync(property);

            if (created)
            {
                var createdProperty = _mapper.Map<PropertyCreated>(property);

                await _publishEndpoint.Publish(createdProperty);
            }

            return property;
        }

        public async Task<bool> Update(Guid propertyId, PropertyDto propertyDto)
        {
            var property = await _rep.GetPropertyAsync(propertyId);

            var propertyToUpdate = _mapper.Map(propertyDto, property);

            propertyToUpdate.CreatedAt = property.CreatedAt;
            propertyToUpdate.UpdatedAt = DateTime.UtcNow;

            var updated = await _rep.UpdateAsync(propertyToUpdate);

            if (updated)
            {
                var updatedProperty = _mapper.Map<PropertyUpdated>(property);

                await _publishEndpoint.Publish(updatedProperty);
            }

            return updated;
        }

        public async Task<bool> Delete(Guid propertyId)
        {
            var property = await _rep.GetPropertyAsync(propertyId);

            if (property.Images.Any())
                await _cloudinaryServices.DeleteImages(property.Images);

            var deleted = await _rep.DeleteAsync(propertyId);

            if (deleted)
            {
                var deletedProperty = new PropertyDeleted { Id = propertyId };

                await _publishEndpoint.Publish(deletedProperty);
            }

            return deleted;
        }

        public async Task UploadImages(List<IFormFile> files, Guid propertyId)
        {
            var property = await _rep.GetPropertyAsync(propertyId);

            if (property != null)
            {
                var images = await _cloudinaryServices.UploadImages(files);

                property.Images = images;

                if (property.Images.Any())
                    await _rep.UpdateAsync(property);
            }
        }
    }
}
