using API.Presenters.Cases;
using API.Presenters.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Presenters.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class CouponController : ControllerBase
    {
        private readonly IGetAllValidCouponsCase getAllValidCouponsCase;

        public CouponController(IGetAllValidCouponsCase getAllValidCouponsCase)
        {
            this.getAllValidCouponsCase = getAllValidCouponsCase;
        }

        [HttpGet]
        [Route("All")]
        [ProducesResponseType(typeof(IList<GetAllValidCouponsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<GetAllValidCouponsResponse> response = getAllValidCouponsCase.Execute();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
