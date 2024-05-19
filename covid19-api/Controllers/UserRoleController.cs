using Microsoft.AspNetCore.Mvc;
using covid19_api.Services.Role;

namespace covid19_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _roleService;
        public UserRoleController(IUserRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<UserRole>>>> GetAllRoles()
        {
            var data = await _roleService.GetAllRoles();
            return Ok(data);
        }
    }
}
