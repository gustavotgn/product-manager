using Product.Domain.DTOs.Supplier;
using System;
using System.ComponentModel;

namespace Product.Domain.DTOs.Product
{
    /// <summary>
    /// Contrato de resposta das informações de um produto
    /// </summary>
    [DisplayName("GetProduct")]
    public record GetProductDTO
    {
        /// <summary>
        /// Identificação
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Descrição
        /// </summary>
        /// <example>Parabrisa Fiat Palio 2012</example>
        public string Description { get; set; }

        /// <summary>
        /// Data de fabricação
        /// </summary>
        /// <example>2024-02-23T16:15:58.410Z</example>
        public DateTime ManufacturingDate { get; set; }

        /// <summary>
        /// Data de validade
        /// </summary>
        /// <example>2024-02-25T16:15:58.410Z</example>
        public DateTime ExpirationDate { get; set; }

        public ListSupplierDTO Supplier { get; set; }
    }
}
