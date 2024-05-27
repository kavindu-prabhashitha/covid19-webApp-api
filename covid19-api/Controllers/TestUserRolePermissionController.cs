using covid19_api.PermissionHandlers;
using Microsoft.AspNetCore.Mvc;

namespace covid19_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestUserRolePermissionController : ControllerBase
    {
        [HttpGet]
        public ActionResult<ServiceResponse<string>> Get()
        {
            var response = new ServiceResponse<string>();
            response.Message = "hello From Permission Test Controller";

            return Ok(response);
        }

        [HttpGet("CreateUser")]
        [HasPermission(Permissions.ADD_USER_DATA)]
        public ActionResult<ServiceResponse<string>> AddUserData()
        {
            var response = new ServiceResponse<string>();
            response.Message = "Permission with 'ADD_USER_DATA' can access this endpoint";

            return Ok(response);
        }

        [HttpGet("UpdateUser")]
        [HasPermission(Permissions.UPDATE_USER_DATA)]
        public ActionResult<ServiceResponse<string>> UpdateUserData()
        {
            var response = new ServiceResponse<string>();
            response.Message = "Permission with 'UPDATE_USER_DATA' can access this endpoint";

            return Ok(response);
        }
    }
}
