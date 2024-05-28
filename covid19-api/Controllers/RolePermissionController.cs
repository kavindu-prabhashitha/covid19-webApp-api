using covid19_api.Dtos.RolePermission;
using covid19_api.Services.Role;
using Microsoft.AspNetCore.Mvc;

namespace covid19_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolePermissionController : ControllerBase
    {
        private readonly IRolePermissionService _permissionService;
        public RolePermissionController(IRolePermissionService permissionService) { 
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetRolePermissionDto>>>> GetRolePermission()
        {
            var data =await  _permissionService.GetAllPermissions();
            return Ok(data);
        }


        [HttpPost]
        public async Task<ActionResult<ServiceResponse<AddRolePermissionDto>>> AddRolePermission(AddRolePermissionDto rolePermissionData)
        {
            var data = await _permissionService.AddRolePermissions(rolePermissionData);
            return Ok(data);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<AddRolePermissionDto>>> UpdateRolePermission(UpdateRolePermissionDto rolePermissionData)
        {
            var response = new ServiceResponse<UpdateRolePermissionDto>();
            response.Data = rolePermissionData;
            return Ok(response);
        }
    }
}
