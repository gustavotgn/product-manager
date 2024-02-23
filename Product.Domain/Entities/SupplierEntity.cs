using System.Collections.Generic;

namespace Product.Domain.Entities
{
    public class SupplierEntity : BaseEntity<int>
    {
        public string Description { get; set; }
        public string NationalRegistration { get; set; }

        public List<ProductEntity> Products { get; set; }
    }
}