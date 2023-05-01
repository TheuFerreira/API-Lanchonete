using API.Domain.Errors;
using API.Presenters.Cases;
using API.Presenters.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Presenters.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        private readonly IGetProductInfoCase getProductInfoCase;
        private readonly IGetAllProductsByCategoriesCase getAllProductsByCategoriesCase;

        public ProductController(IGetProductInfoCase getProductInfoCase, IGetAllProductsByCategoriesCase getAllProductsByCategoriesCase)
        {
            this.getProductInfoCase = getProductInfoCase;
            this.getAllProductsByCategoriesCase = getAllProductsByCategoriesCase;
        }

        [HttpGet]
        [Route("All")]
        [ProducesResponseType(typeof(GetAllProductsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<GetAllProductsResponse> result = getAllProductsByCategoriesCase.Execute();
                return Ok(result);
            }
            catch (BaseEmptyException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("ById/{id}")]
        [ProducesResponseType(typeof(GetProductInfoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBydId(int id)
        {
            try
            {
                GetProductInfoResponse response = getProductInfoCase.Execute(id);
                return Ok(response);
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

        [HttpPost]
        [Route("AllByCategories")]
        [ProducesResponseType(typeof(GetAllProductsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllByCategories(IEnumerable<int> categories)
        {
            try
            {
                IEnumerable<GetAllProductsResponse> response = getAllProductsByCategoriesCase.Execute(categories);
                return Ok(response);
            }
            catch (BaseEmptyException)
            {
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}