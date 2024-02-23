using Product.Domain.DTOs.Product;
using System.Collections.Generic;

namespace Product.Domain.DTOs.Supplier
{
    /// <summary>
    /// Contrato de resposta das informações de um fornecedor
    /// </summary>
    public record GetSupplierDTO
    {
        /// <summary>
        /// Identificação do fornecedor
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// CNPJ
        /// </summary>
        public string NationalRegistration { get; set; }

        /// <summary>
        /// Descrição
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Lista os produtos do fornecedor
        /// </summary>
        public IEnumerable<ListProductDTO> Products { get; set; }
    }
}