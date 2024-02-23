using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Infra.Data.Mappings
{
    internal class SupplierMap : IEntityTypeConfiguration<SupplierEntity>
    {
        public void Configure(EntityTypeBuilder<SupplierEntity> builder)
        {
            builder.ToTable("supplier");

            builder.Property(x => x.Description).HasColumnType("nvarchar(100)");
            builder.Property(x => x.NationalRegistration).HasColumnType("char(14)");
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.DeletedAt);

            builder.HasQueryFilter(x => x.IsActive);
        }
    }
}