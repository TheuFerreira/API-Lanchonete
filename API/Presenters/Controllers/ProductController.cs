using API.Domain.Errors;
using API.Presenters.Cases;
using API.Presenters.Requests;
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllByCategories(GetAllByCategoriesRequest request)
        {
            try
            {
                IEnumerable<GetAllProductsResponse> response = getAllProductsByCategoriesCase.Execute(request.Categories, search: request.Search);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}