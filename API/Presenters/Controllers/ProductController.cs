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

        public ProductController(IGetProductInfoCase getProductInfoCase, IGetAllProductsByCategoriesCase getAllProductsByCategoriesCase, IGetAllProductsBestSellersByCategoriesCase getAllProductsBestSellersByCategoriesCase)
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

        [HttpPost]
        [Route("AllBestSellersByCategories")]
        [ProducesResponseType(typeof(GetAllProductsBestSellersResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllMostPopularByCategories(GetAllBestSellersByCategoriesRequest request)
        {
            try
            {
                IEnumerable<GetAllProductsBestSellersResponse> response = getAllProductsBestSellersByCategoriesCase.Execute(request.Categories, request.Search, request.Limit);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

    public class GetAllBestSellersByCategoriesRequest
    {
        public string Search { get; set; }
        public IEnumerable<int> Categories { get; set; }
        public int Limit { get; set; }

        public GetAllBestSellersByCategoriesRequest()
        {
            Search = string.Empty;
            Categories = new List<int>();
        }
    }
}