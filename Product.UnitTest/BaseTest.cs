using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Product.Domain.Mappers;
using Product.Infra.Data;
using Product.Infra.Data.Repositories;
using Product.Service.Services.v1;

namespace Product.UnitTest
{
    public class BaseTest
    {
        //private others
        private IMapper _mapper;
        
        //private repositories
        private ProductRepository _productRepository;
        private SupplierRepository _supplierRepository;

        //protected others
        protected ApplicationDbContext ApplicationDbContext;
        protected IMapper Mapper => _mapper ??= new MapperConfiguration(cfg => cfg.AddMaps(typeof(ProductMapper))).CreateMapper();
        
        //repositories
        protected ProductRepository ProductRepository => _productRepository ??= new ProductRepository(ApplicationDbContext);
        protected SupplierRepository SupplierRepository => _supplierRepository ??= new SupplierRepository(ApplicationDbContext);
        
        //Services
        protected ProductService ProductService => new(ProductRepository, SupplierRepository, Mapper);
        protected SupplierService SupplierService => new(SupplierRepository, Mapper);

        [TestInitialize]
        public virtual void Init()
        {
            ApplicationDbContext = ApplicationDbContext.GetInMemoryContext();
            ApplicationDbContext.Database.EnsureDeleted();
        }
    }
}
