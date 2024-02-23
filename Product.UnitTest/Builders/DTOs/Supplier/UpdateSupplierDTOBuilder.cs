using Product.Domain.DTOs.Supplier;

namespace Product.UnitTest.Builders.DTOs.Supplier
{
    internal class UpdateSupplierDTOBuilder : BaseBuilder<UpdateSupplierDTO>
    {
        public override UpdateSupplierDTOBuilder Default()
        {
            _instance.Description = "São João";

            return this;
        }

        public UpdateSupplierDTOBuilder WithDescription(string description)
        {
            _instance.Description = description;
            return this;
        }
    }
}
