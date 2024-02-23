using System;
using System.ComponentModel;

namespace Product.Domain.DTOs.Product
{
    /// <summary>
    /// Contrato de request para criação de um novo produto
    /// </summary>
    [DisplayName("UpdateProduct")]
    public record UpdateProductDTO
    {
        /// <summary>Descrição do produto</summary>
        /// <example>Parabrisa Fiat Palio 2012</example>
        public string Description { get; set; }

        /// <summary>
        /// Data de fabricação
        /// </summary>
        /// <example>2024-02-19T16:15:58.410Z</example>
        public DateTime ManufacturingDate { get; set; }


        /// <summary>
        /// Data de validade
        /// </summary>
        /// <example>2024-02-23T16:15:58.410Z</example>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Identificação do fornecedor
        /// </summary>
        /// <example>1</example>
        public int SupplierId { get; set; }
    }
}
