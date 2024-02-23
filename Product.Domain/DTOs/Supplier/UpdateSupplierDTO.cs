using System.ComponentModel;

namespace Product.Domain.DTOs.Supplier
{
    /// <summary>
    /// Contrato de requisição para se alterar um fornecedor
    /// </summary>
    [DisplayName("UpdateSupplier")]
    public record UpdateSupplierDTO()
    {

        /// <summary>
        /// Descrição do fornecedor
        /// </summary>
        /// <example>AutoGlass</example>
        public string Description { get; set; }
    }
}
