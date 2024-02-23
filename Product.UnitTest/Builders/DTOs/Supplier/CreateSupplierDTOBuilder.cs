using Product.Domain.DTOs.Supplier;

namespace Product.UnitTest.Builders.DTOs.Supplier
{
    internal class CreateSupplierDTOBuilder : BaseBuilder<CreateSupplierDTO>
    {
        public override CreateSupplierDTOBuilder Default()
        {
            _instance.Description = "São João";
            _instance.NationalRegistration = "62515101000152";

            return this;
        }

        public CreateSupplierDTOBuilder WithDescription(string description)
        {
            _instance.Description = description;
            return this;
        }
    }
}
