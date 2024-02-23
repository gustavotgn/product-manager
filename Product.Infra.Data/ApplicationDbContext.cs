using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Infra.Data.Initializer;
using Product.Infra.Data.Interceptors;
using Product.Infra.Data.Mappings;

namespace Product.Infra.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly bool _isInMemory;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> Options, bool isInMemory = false) : base(Options)
        {
            _isInMemory = isInMemory;
        }

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<SupplierEntity> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_isInMemory)
                optionsBuilder.UseInMemoryDatabase("productManager");
            else
                optionsBuilder.UseSqlServer("Server=localhost;Database=productManager;Trusted_Connection=true;TrustServerCertificate=True;");

            optionsBuilder
                .AddInterceptors(new SoftDeleteInterceptor());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(ProductMap).Assembly);

            DatabaseInitializer.Seed(modelBuilder);
        }

        public static ApplicationDbContext GetInMemoryContext()
        {
            var databaseName = "product_manager";
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName)
               .Options;

            return new ApplicationDbContext(options, true);
        }
    }
}