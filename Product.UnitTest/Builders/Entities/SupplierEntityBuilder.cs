using Product.Domain.Entities;
using System;

namespace Product.UnitTest.Builders.Entities
{
    internal class SupplierEntityBuilder : BaseBuilder<SupplierEntity>
    {
        public override SupplierEntityBuilder Default()
        {
            _instance.Id = 14;
            _instance.Description = "São João";
            _instance.NationalRegistration = "62515101000152";
            _instance.CreatedAt = DateTime.Now;

            return this;
        }

        internal SupplierEntityBuilder WithId(int id)
        {
            _instance.Id = id;
            return this;
        }

        internal SupplierEntityBuilder WithIsActive(bool isActive)
        {
            _instance.IsActive = isActive;
            return this;
        }
    }
}