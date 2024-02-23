using AutoMapper;
using Product.Domain.DTOs.Supplier;
using Product.Domain.Entities;

namespace Product.Domain.Mappers
{
    public class SupplierMapper : Profile
    {
        public SupplierMapper()
        {
            CreateMap<CreateSupplierDTO, SupplierEntity>();

            CreateMap<UpdateSupplierDTO, SupplierEntity>();

            CreateMap<SupplierEntity, GetSupplierDTO>();

            CreateMap<SupplierEntity, ListSupplierDTO>();
        }
    }
}