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
        private readonly IGetAllProductsBestSellersByCategoriesCase getAllProductsBestSellersByCategoriesCase;

        public ProductController(
            IGetProductInfoCase getProductInfoCase,
            IGetAllProductsByCategoriesCase getAllProductsByCategoriesCase,
            IGetAllProductsBestSellersByCategoriesCase getAllProductsBestSellersByCategoriesCase
        )
        {
            this.getProductInfoCase = getProductInfoCase;
            this.getAllProductsByCategoriesCase = getAllProductsByCategoriesCase;
            this.getAllProductsBestSellersByCategoriesCase = getAllProductsBestSellersByCategoriesCase;
        }

        [HttpGet]
        [Route("ById/{id}")]
        [ProducesResponseType(typeof(GetProductInfoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBydId(int id)
        {
            int userId = 1; // TODO: Remove for Token

            try
            {
                GetProductInfoResponse response = getProductInfoCase.Execute(id, userId);
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllByCategories(GetAllByCategoriesRequest request)
        {
            int userId = 1; // TODO: Remove for Token

            try
            {
                IEnumerable<GetAllProductsResponse> response = getAllProductsByCategoriesCase.Execute(request.Categories, search: request.Search, userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("AllBestSellersByCategories")]
        [ProducesResponseType(typeof(GetAllProductsBestSellersResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllMostPopularByCategories(GetAllBestSellersByCategoriesRequest request)
        {
            int userId = 1; // TODO: Remove for Token

            try
            {
                IEnumerable<GetAllProductsBestSellersResponse> response = getAllProductsBestSellersByCategoriesCase.Execute(request.Categories, request.Search, request.Limit, userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}