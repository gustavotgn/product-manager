using System;
using System.ComponentModel;

namespace Product.Domain.DTOs.Product
{
    /// <summary>
    /// Contrato de resposta das informações de um produto
    /// </summary>
    [DisplayName("ListProduct")]
    public record ListProductDTO
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
        public DateTime ManufacturingDate { get; set; }

        /// <summary>
        /// Data de validade
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Id do fornecedor
        /// </summary>
        public int SupplierId { get; set; }
    }
}
