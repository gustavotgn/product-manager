using Product.Domain.Entities;
using System;

namespace Product.UnitTest.Builders.Entities
{
    internal class ProductEntityBuilder : BaseBuilder<ProductEntity>
    {
        public override ProductEntityBuilder Default()
        {
            _instance.Id = 15;
            _instance.Description = "Arroz São João";
            _instance.ManufacturingDate = DateTime.Now;
            _instance.ExpirationDate = DateTime.Now.AddDays(100);
            _instance.CreatedAt = DateTime.Now;
            _instance.IsActive = true;

            return this;
        }

        public ProductEntityBuilder WithDescription(string description)
        {
            _instance.Description = description;
            return this;
        }

        public ProductEntityBuilder WithManufacturingDate(DateTime manufacturingDate)
        {
            _instance.ManufacturingDate = manufacturingDate;
            return this;
        }

        public ProductEntityBuilder WithExpirationDate(DateTime expirationDate)
        {
            _instance.ExpirationDate = expirationDate;
            return this;
        }

        internal ProductEntityBuilder WithIsActive(bool isActive)
        {
            _instance.IsActive = isActive;
            return this;
        }

        internal ProductEntityBuilder WithSupplierId(int supplierId)
        {
            _instance.SupplierId = supplierId;
            return this;
        }

        internal ProductEntityBuilder WithId(int id)
        {
            _instance.Id = id;
            return this;
        }
    }
}