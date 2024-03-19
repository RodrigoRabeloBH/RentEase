using Microsoft.AspNetCore.Mvc;
using RentEase.Domain.Dto;
using RentEase.Domain.Entities;
using RentEase.Domain.Interfaces.Services;

namespace RentEase.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyServices _services;

        public PropertiesController(IPropertyServices services)
        {
            _services = services;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Property>> Get(Guid id)
        {
            var property = await _services.GetProperty(id);

            return property;
        }

        [HttpGet]
        public async Task<ActionResult<List<Property>>> GetAll()
        {
            var properties = await _services.GetProperties();

            return properties;
        }

        [HttpPost]
        public async Task<ActionResult> Create(PropertyDto propertyDto)
        {
            var property = await _services.Create(propertyDto);

            return CreatedAtAction(nameof(Get), new { id = property.Id }, property);
        }

        [HttpPut("{propertyId}")]
        public async Task<ActionResult<bool>> Update(PropertyDto propertyDto, Guid propertyId)
        {
            var updated = await _services.Update(propertyId, propertyDto);

            if (!updated)
                return BadRequest("Failed to update property");

            return updated;
        }

        [HttpDelete("{propertyId}")]
        public async Task<ActionResult<bool>> Delete(Guid propertyId)
        {
            var deleted = await _services.Delete(propertyId);

            if (!deleted)
                return BadRequest("Failed to delete property");

            return deleted;
        }

        [HttpPost("images/{propertyId}")]
        public async Task<ActionResult> UploadImages(List<IFormFile> files, Guid propertyId)
        {
            await _services.UploadImages(files, propertyId);

            return Ok();
        }
    }
}
