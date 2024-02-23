using Product.Domain.DTOs.Product;
using System;

namespace Product.UnitTest.Builders.DTOs.Product
{
    internal class CreateProductDTOBuilder : BaseBuilder<CreateProductDTO>
    {
        public override CreateProductDTOBuilder Default()
        {
            _instance.Description = "Arroz São João";
            _instance.ManufacturingDate = DateTime.Now;
            _instance.ExpirationDate = DateTime.Now.AddDays(100);
            _instance.SupplierId = 14;

            return this;
        }

        public CreateProductDTOBuilder WithDescription(string description)
        {
            _instance.Description = description;
            return this;
        }

        public CreateProductDTOBuilder WithManufacturingDate(DateTime manufacturingDate)
        {
            _instance.ManufacturingDate = manufacturingDate;
            return this;
        }

        public CreateProductDTOBuilder WithExpirationDate(DateTime expirationDate)
        {
            _instance.ExpirationDate = expirationDate;
            return this;
        }
        internal CreateProductDTOBuilder WithSupplierId(int supplierId)
        {
            _instance.SupplierId = supplierId;
            return this;
        }
    }
}
