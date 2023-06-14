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
        private readonly IDeleteProductFromCartCase deleteProductFromCartCase;

        public CartProductController(ISaveProductToCartCase saveProductToCartCase, ICountCartProductsCase countCartProductsCase, IDeleteProductFromCartCase deleteProductFromCartCase)
        {
            this.saveProductToCartCase = saveProductToCartCase;
            this.countCartProductsCase = countCartProductsCase;
            this.deleteProductFromCartCase = deleteProductFromCartCase;
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

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            int userId = 1; // TODO: Remove for token

            try
            {
                deleteProductFromCartCase.Execute(userId, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
