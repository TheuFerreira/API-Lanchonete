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
        private readonly IFavoriteProductCase favoriteProductCase;
        private readonly ISearchFavoritesCase searchFavoritesCase;

        public ProductController(
            IGetProductInfoCase getProductInfoCase,
            IGetAllProductsByCategoriesCase getAllProductsByCategoriesCase,
            IGetAllProductsBestSellersByCategoriesCase getAllProductsBestSellersByCategoriesCase,
            IFavoriteProductCase favoriteProductCase,
            ISearchFavoritesCase searchFavoritesCase
        )
        {
            this.getProductInfoCase = getProductInfoCase;
            this.getAllProductsByCategoriesCase = getAllProductsByCategoriesCase;
            this.getAllProductsBestSellersByCategoriesCase = getAllProductsBestSellersByCategoriesCase;
            this.favoriteProductCase = favoriteProductCase;
            this.searchFavoritesCase = searchFavoritesCase;
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

        [HttpPut]
        [Route("Favorite")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateFavorite(UpdateFavoriteRequest request)
        {
            int userId = 1; // TODO: Remove for Token

            try
            {
                bool newValue = favoriteProductCase.Execute(userId, request.ProductId);
                return Ok(newValue);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("SearchFavorites")]
        [ProducesResponseType(typeof(IEnumerable<SearchFavoritesResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult SearchFavorites(SearchFavoritesRequest request)
        {
            int userId = 1;

            try
            {
                IEnumerable<SearchFavoritesResponse> response = searchFavoritesCase.Execute(userId, request.Search);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}