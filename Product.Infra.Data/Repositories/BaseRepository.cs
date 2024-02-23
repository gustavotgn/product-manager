using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Infra.Data.Repositories
{
    public abstract class BaseRepository<T, TId> where T : BaseEntity<TId>
    {
        protected ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        protected DbSet<T> DbSet => _context.Set<T>();

        public async Task<T?> SelectAsync(TId id) => await _context.Set<T>().FindAsync(id);

        public async Task InsertAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task InsertRangeAsync(IEnumerable<T> entity)
        {
            _context.Set<T>().AddRange(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entity)
        {
            _context.Set<T>().UpdateRange(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TId id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().RemoveRange(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entity)
        {
            _context.Set<T>().RemoveRange(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(TId id)
        {
            return await _context.Set<T>().AnyAsync(x => x.Id.Equals(id));
        }
    }
}
