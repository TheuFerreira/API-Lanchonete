using API.Domain.Errors;
using API.Presenters.Cases;
using API.Presenters.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Presenters.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class LabelController : ControllerBase
    {
        private readonly IGetAllLabelsCase getAllLabelsCase;

        public LabelController(IGetAllLabelsCase getAllLabelsCase)
        {
            this.getAllLabelsCase = getAllLabelsCase;
        }

        [HttpGet]
        [Route("All")]
        [ProducesResponseType(typeof(IList<GetAllLabelsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<GetAllLabelsResponse> response = getAllLabelsCase.Execute();
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
