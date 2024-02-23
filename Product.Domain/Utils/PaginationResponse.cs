using System.Collections.Generic;

namespace Product.Domain.Utils
{
    /// <summary>
    /// Contrato de resposta para requisições paginadas
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public record PaginationResponse<T>
    {
        public PaginationResponse()
        {

        }

        public PaginationResponse(IEnumerable<T> items, int total)
        {
            Items = items;
            Total = total;
        }

        /// <summary>
        /// Itens da página
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Total de itens cadastrado
        /// </summary>
        public int Total { get; set; }
    }
}