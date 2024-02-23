using Microsoft.AspNetCore.Mvc;
using Product.Domain.Utils;

namespace Product.Api.Controllers
{
    /// <summary>
    /// Classe base para concentrar regras gerais aplicadas a todas as controllers
    /// </summary>
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult Send(Result result)
        {
            if (result.Success)
                return Ok(result);

            return BadRequest(result.Errors);
        }

        protected IActionResult Send<T>(Result<T> result) where T : class
        {
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Errors);
        }
    }
}
