using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentEase.Domain.Entities;
using RentEase.Domain.Interfaces.Repositories;
using RentEase.Infrastructure.Data;
using System.Diagnostics.CodeAnalysis;

namespace RentEase.Infrastructure.Repositories
{
    [ExcludeFromCodeCoverage]
    public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(RentEaseContext context, ILogger<BaseRepository<Property>> logger)
            : base(context, logger) { }

        public async Task<List<Property>> GetPropertiesAsync()
        {
            try
            {
                var properties = await _context.Properties
                    .Include(p => p.SellerInfo)
                    .Include(p => p.Location)
                    .Include(p => p.Images)            
                    .Include(p => p.Rates)
                    .AsNoTracking()
                    .ToListAsync();

                return properties;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[PROPERTY REPOSITORY]-[GET PROPERTIES] --> Error message: {message}", ex.Message);

                throw;
            }
        }

        public async Task<Property> GetPropertyAsync(Guid id)
        {
            try
            {
                var property = await _context.Properties
                    .Include(p => p.SellerInfo)
                    .Include(p => p.Location)
                    .Include(p => p.Images)
                    .Include(p => p.Rates)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == id);

                return property;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[PROPERTY REPOSITORY]-[GET PROPERTY] --> Error message: {message}", ex.Message);

                throw;
            }
        }
    }
}
