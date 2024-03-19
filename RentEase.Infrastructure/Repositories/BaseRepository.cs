using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Win32.SafeHandles;
using RentEase.Domain.Entities;
using RentEase.Domain.Interfaces.Repositories;
using RentEase.Infrastructure.Data;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace RentEase.Infrastructure.Repositories
{
    [ExcludeFromCodeCoverage]
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        protected readonly RentEaseContext _context;
        protected readonly ILogger<BaseRepository<T>> _logger;

        private bool _disposedValue;

        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        public BaseRepository(RentEaseContext context, ILogger<BaseRepository<T>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            try
            {
                return await _context.Set<T>()
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                LogError(ex);

                throw;
            }
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.Set<T>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Id == id);
            }
            catch (Exception ex)
            {
                LogError(ex);

                throw;
            }
        }
        public virtual async Task<bool> CreateAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                LogError(ex);

                throw;
            }
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                LogError(ex);

                throw;
            }
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await GetByIdAsync(id);

                _context.Set<T>().Remove(entity);

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                LogError(ex);

                throw;
            }
        }

        public virtual async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _safeHandle?.Dispose();
                    _safeHandle = null;
                }

                _disposedValue = true;
            }
        }
        private void LogError(Exception ex)
        {
            _logger.LogError(ex, "[BASE REPOSITORY] --> Erro message: {message}", ex.Message);
        }
    }
}
