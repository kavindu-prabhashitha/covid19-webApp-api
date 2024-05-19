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

        [HttpGet("admin-protected")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<ActionResult> GetAdminProtected()
        {
            return Ok("Hello from Admin Only Route");
        }

        [HttpGet("user-protected")]
        [Authorize(Roles = "USER")]
        public async Task<ActionResult> GetUserProtected()
        {
            return Ok("Hello from User Only Route");
        }

        [HttpGet("user-admin-protected")]
        [Authorize(Roles = "USER")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<ActionResult> GetUserAdminProtected()
        {
            return Ok("Hello from User and Admin Only Route");
        }

    }
}
