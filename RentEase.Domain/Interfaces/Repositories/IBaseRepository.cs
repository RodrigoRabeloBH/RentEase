using RentEase.Domain.Entities;

namespace RentEase.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> : IDisposable where T : Entity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task<bool> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> SaveAsync();
    }
}
