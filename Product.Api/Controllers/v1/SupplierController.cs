using Microsoft.AspNetCore.Mvc;
using Product.Api.Controllers;
using Product.Domain.DTOs.Supplier;
using Product.Domain.Utils;
using Product.Service.Services.v1;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Supplier.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SupplierController : BaseController
    {
        private readonly SupplierService _service;

        public SupplierController(SupplierService supplierService) => _service = supplierService;

        /// <summary>
        /// Adiciona um novo fornecedor.
        /// </summary>
        /// <returns>O fornecedor cadastrado</returns>
        /// <response code="200">Retorna o objeto do fornecedor cadastrado</response>
        /// <response code="400">Retorna uma lista de erros</response>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GetSupplierDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error[]), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateSupplierDTO body)
            => Send(await _service.CreateAsync(body));

        /// <summary>
        /// Busca as informações do fornecedor.
        /// </summary>
        /// <returns>O fornecedor</returns>        
        /// <param name="id">Identificação do fornecedor</param>
        /// <response code="200">Retorna o objeto do fornecedor</response>
        /// <response code="400">Retorna uma lista de erros</response>
        [HttpGet("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GetSupplierDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error[]), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
            => Send(await _service.GetAsync(id));

        /// <summary>
        /// Lista as informações do fornecedor.
        /// </summary>
        /// <returns>O fornecedor cadastrado</returns>
        /// <param name="filter">Filtro para a pesquisa</param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <response code="200">Retorna o objeto do fornecedor cadastrado</response>
        /// <response code="400">Retorna uma lista de erros</response>
        /// <returns></returns>
        [HttpGet()]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<ListSupplierDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error[]), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ListAsync([FromQuery] string filter, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
            => Send(await _service.ListAsync(filter, page, pageSize));

        /// <summary>
        /// Exclui o fornecedor.
        /// </summary>
        /// <returns></returns>        
        /// <param name="id">Identificação do fornecedor</param>
        /// <response code="200"></response>
        /// <response code="400">Retorna uma lista de erros</response>
        [HttpDelete("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error[]), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
            => Send(await _service.DeleteAsync(id));
    }
}