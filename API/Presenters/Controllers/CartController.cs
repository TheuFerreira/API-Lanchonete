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
    public class CartController : ControllerBase
    {
        private readonly IAddProductToCartCase addProductToCartCase;

        public CartController(IAddProductToCartCase addProductToCartCase)
        {
            this.addProductToCartCase = addProductToCartCase;
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
    }
}
