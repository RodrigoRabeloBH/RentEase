using RentEase.Domain.Entities;

namespace RentEase.Domain.Interfaces.Repositories
{
    public interface IPropertyRepository : IBaseRepository<Property>
    {
        Task<Property> GetPropertyAsync(Guid id);
        Task<List<Property>> GetPropertiesAsync();
    }
}
