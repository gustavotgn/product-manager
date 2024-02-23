using Product.Domain.DTOs.Product;
using System;

namespace Product.UnitTest.Builders.DTOs.Product
{
    internal class GetProductDTOBuilder : BaseBuilder<GetProductDTO>
    {
        public override GetProductDTOBuilder Default()
        {
            _instance.Description = "Arroz São João";
            _instance.ManufacturingDate = DateTime.Now;
            _instance.ExpirationDate = DateTime.Now.AddDays(100);

            return this;
        }

        public GetProductDTOBuilder WithDescription(string description)
        {
            _instance.Description = description;
            return this;
        }

        public GetProductDTOBuilder WithManufacturingDate(DateTime manufacturingDate)
        {
            _instance.ManufacturingDate = manufacturingDate;
            return this;
        }

        public GetProductDTOBuilder WithExpirationDate(DateTime expirationDate)
        {
            _instance.ExpirationDate = expirationDate;
            return this;
        }
    }
}
