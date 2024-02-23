using Microsoft.AspNetCore.Mvc;
using Product.Domain.DTOs.Product;
using Product.Domain.Utils;
using Product.Service.Services.v1;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Product.Api.Controllers.v1
{
    /// <summary>
    /// Produto
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : BaseController
    {
        readonly ProductService _service;

        /// <param name="service"></param>
        public ProductController(ProductService service)
        {
            _service = service;
        }

        /// <summary>
        /// Adiciona um novo produto.
        /// </summary>
        /// <returns>O produto cadastrado</returns>
        /// <response code="200">Retorna o objeto do produto cadastrado</response>
        /// <response code="400">Retorna uma lista de erros</response>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GetProductDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error[]), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateProductDTO body)
            => Send(await _service.CreateAsync(body));

        /// <summary>
        /// Busca as informações do produto.
        /// </summary>
        /// <returns>O produto</returns>        
        /// <param name="id">Identificação do produto</param>
        /// <response code="200">Retorna o objeto do produto</response>
        /// <response code="400">Retorna uma lista de erros</response>
        [HttpGet("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GetProductDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error[]), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
            => Send(await _service.GetAsync(id));

        /// <summary>
        /// Lista as informações do produto.
        /// </summary>
        /// <returns>O produto cadastrado</returns>
        /// <param name="filter">Filtro para a pesquisa</param>
        /// <response code="200">Retorna o objeto do produto cadastrado</response>
        /// <response code="400">Retorna uma lista de erros</response>
        /// <returns></returns>
        [HttpGet()]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<ListProductDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error[]), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ListAsync([FromQuery] string filter, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
            => Send(await _service.ListAsync(filter, page, pageSize));

        /// <summary>
        /// Exclui o produto.
        /// </summary>
        /// <returns></returns>        
        /// <param name="id">Identificação do produto</param>
        /// <response code="200"></response>
        [HttpDelete("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error[]), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
            => Send(await _service.DeleteAsync(id));

        /// <summary>
        /// Altera as informações do produto.
        /// </summary>
        /// <returns>O produto</returns>        
        /// <param name="id">Identificação do produto</param>
        /// <param name="body"></param>
        /// <response code="200">Retorna o objeto do produto</response>
        /// <response code="400">Retorna uma lista de erros</response>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GetProductDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error[]), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsync([FromRoute] int id, [FromBody] UpdateProductDTO body)
            => Send(await _service.UpdateAsync(id, body));
    }
}