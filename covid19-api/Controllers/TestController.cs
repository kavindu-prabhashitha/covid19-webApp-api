using covid19_api.Dtos.CountryData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace covid19_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<ServiceResponse<string>> Get()
        {
            var response = new ServiceResponse<string>();
            response.Message = "Authorized User";

            return Ok(response);
        }

    }
}
