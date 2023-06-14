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
        private readonly IAddProductToCartCase addProductToCartCase;
        private readonly ICountCartProductsCase countCartProductsCase;

        public CartProductController(IAddProductToCartCase addProductToCartCase, ICountCartProductsCase countCartProductsCase)
        {
            this.addProductToCartCase = addProductToCartCase;
            this.countCartProductsCase = countCartProductsCase;
        }

        [HttpPost]
        [Route("Add")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Add(CartAddRequest request)
        {
            int userId = 1; // TODO: Remove for token

            try
            {
                request.UserId = userId;

                addProductToCartCase.Execute(request);
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
