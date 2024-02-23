using Product.Domain.DTOs.Product;
using System;

namespace Product.UnitTest.Builders.DTOs.Product
{
    internal class UpdateProductDTOBuilder : BaseBuilder<UpdateProductDTO>
    {
        public override UpdateProductDTOBuilder Default()
        {
            _instance.Description = "Arroz São João";
            _instance.ManufacturingDate = DateTime.Now;
            _instance.ExpirationDate = DateTime.Now.AddDays(100);
            _instance.SupplierId = 14;

            return this;
        }

        public UpdateProductDTOBuilder WithDescription(string description)
        {
            _instance.Description = description;
            return this;
        }

        public UpdateProductDTOBuilder WithManufacturingDate(DateTime manufacturingDate)
        {
            _instance.ManufacturingDate = manufacturingDate;
            return this;
        }

        public UpdateProductDTOBuilder WithExpirationDate(DateTime expirationDate)
        {
            _instance.ExpirationDate = expirationDate;
            return this;
        }
        internal UpdateProductDTOBuilder WithSupplierId(int supplierId)
        {
            _instance.SupplierId = supplierId;
            return this;
        }
    }
}
