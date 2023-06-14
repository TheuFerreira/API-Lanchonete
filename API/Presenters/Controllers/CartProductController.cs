using API.Domain.Errors;
using API.Presenters.Cases;
using API.Presenters.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Presenters.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class CartProductController : ControllerBase
    {
        private readonly ISaveProductToCartCase saveProductToCartCase;
        private readonly ICountCartProductsCase countCartProductsCase;

        public CartProductController(ISaveProductToCartCase saveProductToCartCase, ICountCartProductsCase countCartProductsCase)
        {
            this.saveProductToCartCase = saveProductToCartCase;
            this.countCartProductsCase = countCartProductsCase;
        }

        [HttpPost]
        [Route("Save")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Save(CartAddRequest request)
        {
            int userId = 1; // TODO: Remove for token

            try
            {
                request.UserId = userId;

                saveProductToCartCase.Execute(request);
                return Ok();
            }
            catch (BaseInvalidException)
            {
                return BadRequest();
            }
            catch (BaseNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("Count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Count()
        {
            int userId = 1; // TODO: Remove for token

            try
            {
                int count = countCartProductsCase.Execute(userId);
                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
