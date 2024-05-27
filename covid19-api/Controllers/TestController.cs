using covid19_api.Dtos.CountryData;
using covid19_api.Services.UserClaims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace covid19_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IUserClaimsService _userClaimsService;
        public TestController(IUserClaimsService userClaimsService) {
            _userClaimsService = userClaimsService;
        }

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


        [HttpGet("get-claim-data")]
        [Authorize]
        public async Task<ActionResult> GetClaimsData()
        {
            var userName = User?.Identity?.Name;
            var userRole = User?.FindFirstValue(ClaimTypes.Role);
            var userRoleClaims = User?.FindAll(ClaimTypes.Role);
            var roles = userRoleClaims?.Select(c => c.Value).ToList();
            return Ok(new {userName,userRole,roles});
        }

        [HttpGet("get-claim-data-from-service")]
        [Authorize]
        public async Task<ActionResult> GetClaimsDataFromService()
        {
            var userName = _userClaimsService.GetUserName();
            var userRole = _userClaimsService.GetUserRole();
            var userRoleClaims = User?.FindAll(ClaimTypes.Role);
            var roles = userRoleClaims?.Select(c => c.Value).ToList();
            return Ok(new { userName, userRole, roles });
        }

    }
}
