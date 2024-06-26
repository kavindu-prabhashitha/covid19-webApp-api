﻿using Microsoft.AspNetCore.Mvc;
using covid19_api.Services.Role;
using covid19_api.Dtos.UserRole;

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

        [HttpGet("GetRoleById")]
        public async Task<ActionResult<ServiceResponse<UserRole>>> GetRoleById(int id)
        {
            var data = await _roleService.GetRoleById(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<AddUserRoleDto>>> AddUserRole(AddUserRoleDto roleData)
        {
 

            if (roleData != null)
            {
                var data = await _roleService.AddUserRole(roleData);
                return Ok(data);
            }

            return BadRequest("Operation Failed");    
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetUserRoleDto>>> UpdateUserRole(UpdateRoleDto roleData)
        {

            var response = await _roleService.UpdateRole(roleData);
            if (response is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost("add-permissions-to-role")]
        public async Task<ActionResult<ServiceResponse<UserRole>>> AddPermissionsToUserRole(AddPermissionsToUserRoleDto roleData)
        {


            if (roleData != null)
            {
                var data = await _roleService.AddPermissionsForUserRole(roleData);
                return Ok(data);
            }

            return BadRequest("Operation Failed");
        }

        [HttpPost("remove-permissions-from-role")]
        public async Task<ActionResult<ServiceResponse<UserRole>>> RemovePermissionsFromUserRole(RemovePermissionFromUserRoleDto roleData)
        {


            if (roleData != null)
            {
                var data = await _roleService.RemovePermissionsFromUserRole(roleData);
                return Ok(data);
            }

            return BadRequest("Operation Failed");
        }

    }
}
