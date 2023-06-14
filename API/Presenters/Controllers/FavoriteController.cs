using API.Presenters.Cases;
using API.Presenters.Requests;
using API.Presenters.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Presenters.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteProductCase favoriteProductCase;
        private readonly ISearchFavoritesCase searchFavoritesCase;
        private readonly ICountFavoritesCase countFavoritesCase;

        public FavoriteController(
            IFavoriteProductCase favoriteProductCase,
            ISearchFavoritesCase searchFavoritesCase,
            ICountFavoritesCase countFavoritesCase
        )
        {
            this.favoriteProductCase = favoriteProductCase;
            this.searchFavoritesCase = searchFavoritesCase;
            this.countFavoritesCase = countFavoritesCase;
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
        [Route("Search")]
        [ProducesResponseType(typeof(IEnumerable<SearchFavoritesResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Search(SearchFavoritesRequest request)
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

        [HttpGet]
        [Route("Count")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCount()
        {
            int userId = 1;

            try
            {
                int count = countFavoritesCase.Execute(userId);
                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
