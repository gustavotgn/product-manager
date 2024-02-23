using AutoMapper;
using Product.Domain.DTOs.Product;
using Product.Domain.Entities;
using Product.Domain.Error;
using Product.Domain.Utils;
using Product.Infra.Data.Repositories;
using Product.Service.Validators.v1.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Service.Services.v1
{
    /// <summary>
    /// Service responsável por controlar o Produto
    /// </summary>
    public class ProductService : BaseService
    {
        private readonly ProductRepository _productRepository;
        private readonly SupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public ProductService(ProductRepository productRepository, SupplierRepository supplierRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Cadastra um novo produto
        /// </summary>
        /// <param name="createProduct"></param>
        /// <returns></returns>
        public async Task<Result<GetProductDTO>> CreateAsync(CreateProductDTO createProduct)
        {
            var validator = await new CreateProductValidator().ValidateAsync(createProduct);

            if (!validator.IsValid)
                return Error<GetProductDTO>(validator.Errors);

            var isSupplierIdValid = await _supplierRepository.AnyAsync(createProduct.SupplierId);

            if (!isSupplierIdValid)
                return Error<GetProductDTO>(EErrors.ProductCreateAsyncSupplierIdInvalid);

            var entity = _mapper.Map<ProductEntity>(createProduct);

            var isExists = await _productRepository.AnyAsync(
                createProduct.Description,
                createProduct.ManufacturingDate,
                createProduct.ExpirationDate,
                createProduct.SupplierId);

            if (isExists)
                return Error<GetProductDTO>(EErrors.ProductCreateAsyncProductAlreadyExists);

            await _productRepository.InsertAsync(entity);

            return await GetAsync(entity.Id);
        }

        /// <summary>
        /// Deleta um produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result> DeleteAsync(int id)
        {
            var entity = await _productRepository.SelectAsync(id);

            if (entity == null)
                return Error<GetProductDTO>(EErrors.ProductDeleteAsyncProductNotFound);

            await _productRepository.DeleteAsync(entity);

            return Success();
        }

        /// <summary>
        /// Busca o detalhe do produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<GetProductDTO>> GetAsync(int id)
        {
            var entity = await _productRepository.SelectWithIncludesAsync(id);

            if (entity == null)
                return Error<GetProductDTO>(EErrors.ProductGetAsyncProductNotFound);

            return Success(_mapper.Map<GetProductDTO>(entity));
        }

        /// <summary>
        /// Busca uma lista de produtos
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<Result<PaginationResponse<ListProductDTO>>> ListAsync(string filter, int page = 1, int pageSize = 10)
        {
            var (entities, count) = await _productRepository.SelectAsync(filter, page, pageSize);

            var dtos = _mapper.Map<List<ListProductDTO>>(entities);

            return Success(new PaginationResponse<ListProductDTO>(dtos, count));
        }

        /// <summary>
        /// Altera um produto a partir do <paramref name="id"/>
        /// </summary>
        /// <param name="updateProduct"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<GetProductDTO>> UpdateAsync(int id, UpdateProductDTO updateProduct)
        {
            var entity = await _productRepository.SelectAsync(id);

            if (entity == null)
                return Error<GetProductDTO>(EErrors.ProductUpdateAsyncProductNotFound);

            entity = _mapper.Map(updateProduct, entity);

            await _productRepository.UpdateAsync(entity);

            return await GetAsync(id);
        }
    }
}