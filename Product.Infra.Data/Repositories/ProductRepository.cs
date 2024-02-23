using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Infra.Data.Repositories
{
    public class ProductRepository : BaseRepository<ProductEntity, int>
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> AnyAsync(string description, DateTime manufacturingDate, DateTime expirationDate, int supplierId) =>
            await DbSet.AnyAsync(x =>
                x.Description.Equals(description) &&
                x.ManufacturingDate.Equals(manufacturingDate) &&
                x.ExpirationDate.Equals(expirationDate) &&
                x.SupplierId.Equals(supplierId));

        public async Task<(List<ProductEntity>, int)> SelectAsync(string filter, int page = 1, int pageSize = 10)
        {
            var query = DbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
                query = query.Where(x => x.Description.Contains(filter));

            var total = query.Count();

            var entities = await query.OrderBy(x => x.Description)
             .Skip((page - 1) * pageSize)
             .Take(pageSize).ToListAsync();

            return (entities, total);
        }

        public async Task<ProductEntity> SelectWithIncludesAsync(int id) =>
            await DbSet
                .Include(x => x.Supplier)
                .FirstOrDefaultAsync(x => x.Id == id);
    }
}