using System;

namespace Product.Domain.Entities
{
    public class ProductEntity : BaseEntity<int>
    {
        public string Description { get; set; }

        public DateTime ManufacturingDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public SupplierEntity Supplier { get; set; }
        public int SupplierId { get; set; }
    }
}