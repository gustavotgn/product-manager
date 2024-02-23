using AutoMapper;
using Product.Domain.DTOs.Product;
using Product.Domain.Entities;

namespace Product.Domain.Mappers
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<CreateProductDTO, ProductEntity>();

            CreateMap<UpdateProductDTO, ProductEntity>();

            CreateMap<ProductEntity, GetProductDTO>();

            CreateMap<ProductEntity, ListProductDTO>();
        }
    }
}