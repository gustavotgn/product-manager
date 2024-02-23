using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Infra.Data.Repositories
{
    public class SupplierRepository : BaseRepository<SupplierEntity, int>
    {
        public SupplierRepository(ApplicationDbContext context) : base(context)
        { }

        public async Task<(List<SupplierEntity>, int)> SelectAsync(string filter, int page = 1, int pageSize = 10)
        {
            var query = DbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
                query = query.Where(x => x.Description.Contains(filter) || x.NationalRegistration.Contains(filter));

            var total = query.Count();

            var entities = await query.OrderBy(x => x.Description)
             .Skip((page - 1) * pageSize)
             .Take(pageSize).ToListAsync();

            return (entities, total);
        }

        public new async Task DeleteAsync(SupplierEntity entity)
        {
            var products = await _context.Products.Where(x => x.SupplierId.Equals(entity.Id)).ToListAsync();
            
            if (products.Any())
                _context.Products.RemoveRange(products);

            DbSet.Remove(entity);

            _context.SaveChanges();
        }

        public async Task<SupplierEntity> SelectWithIncludesAsync(int id) => 
            await DbSet.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id.Equals(id));

        public async Task<bool> AnyAsync(string nationalRegistration)
        {
            return await DbSet.AnyAsync(x => x.NationalRegistration.Equals(nationalRegistration));
        }
    }
}