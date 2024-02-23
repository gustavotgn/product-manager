using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Infra.Data.Mappings
{
    internal class ProductMap : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("product");

            builder.Property(x => x.Description).HasColumnType("nvarchar(100)");
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.DeletedAt);

            builder.HasQueryFilter(x => x.IsActive);
        }
    }
}