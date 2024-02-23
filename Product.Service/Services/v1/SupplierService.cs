using AutoMapper;
using Product.Domain.DTOs.Supplier;
using Product.Domain.Entities;
using Product.Domain.Error;
using Product.Domain.Utils;
using Product.Infra.Data.Repositories;
using Product.Service.Validators.v1.Supplier;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Service.Services.v1
{
    /// <summary>
    /// Service responsável por controlar o Fornecedor
    /// </summary>
    public class SupplierService : BaseService
    {
        private readonly SupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public SupplierService(SupplierRepository supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Cria um fornecedor
        /// </summary>
        /// <param name="createSupplier"></param>
        /// <returns></returns>
        public async Task<Result<GetSupplierDTO>> CreateAsync(CreateSupplierDTO createSupplier)
        {
            var validator = await new CreateSupplierValidator().ValidateAsync(createSupplier);

            if (!validator.IsValid)
                return Error<GetSupplierDTO>(validator.Errors);

            var entity = _mapper.Map<SupplierEntity>(createSupplier);
            
            var isExists = await _supplierRepository.AnyAsync(createSupplier.NationalRegistration);

            if (isExists)
                return Error<GetSupplierDTO>(EErrors.SupplierCreateAsyncSupplierAlreadyExists);

            await _supplierRepository.InsertAsync(entity);

            var getSupplierDto = _mapper.Map<GetSupplierDTO>(entity);

            return Success(getSupplierDto);
        }

        /// <summary>
        /// Deleta um fornecedor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result> DeleteAsync(int id)
        {
            var entity = await _supplierRepository.SelectAsync(id);

            if (entity == null)
                return Error<GetSupplierDTO>(EErrors.SupplierDeleteAsyncSupplierNotFound);

            await _supplierRepository.DeleteAsync(entity);

            return Success();
        }

        /// <summary>
        /// Busca o detalhe do fornecedor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<GetSupplierDTO>> GetAsync(int id)
        {
            var entity = await _supplierRepository.SelectWithIncludesAsync(id);

            if (entity == null)
                return Error<GetSupplierDTO>(EErrors.SupplierGetAsyncSupplierNotFound);

            return Success(_mapper.Map<GetSupplierDTO>(entity));
        }

        /// <summary>
        /// Busca uma lista de fornecedors
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<Result<PaginationResponse<ListSupplierDTO>>> ListAsync(string filter, int page = 1, int pageSize = 10)
        {
            var (entities, count) = await _supplierRepository.SelectAsync(filter, page, pageSize);

            var dtos = _mapper.Map<List<ListSupplierDTO>>(entities);

            return Success(new PaginationResponse<ListSupplierDTO>(dtos, count));
        }

        /// <summary>
        /// Altera um fornecedor a partir do <paramref name="id"/>
        /// </summary>
        /// <param name="updateSupplier"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<GetSupplierDTO>> UpdateAsync(int id, UpdateSupplierDTO updateSupplier)
        {
            var entity = await _supplierRepository.SelectAsync(id);

            if (entity == null)
                return Error<GetSupplierDTO>(EErrors.SupplierUpdateAsyncSupplierNotFound);

            entity = _mapper.Map(updateSupplier, entity);

            await _supplierRepository.UpdateAsync(entity);

            return await GetAsync(id);
        }
    }
}
