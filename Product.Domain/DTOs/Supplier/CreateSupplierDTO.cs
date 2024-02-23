using System.ComponentModel;

namespace Product.Domain.DTOs.Supplier
{
    /// <summary>
    /// Contrato de requisição para se criar um novo fornecedor
    /// </summary>
    [DisplayName("CreateSupplier")]
    public record CreateSupplierDTO()
    {
        /// <summary>
        /// CNPJ válido do fornecedor
        /// </summary>
        /// <example>62515101000152</example>
        public string NationalRegistration { get; set; }

        /// <summary>
        /// Descrição do fornecedor
        /// </summary>
        /// <example>AutoGlass</example>
        public string Description { get; set; }
    }
}
