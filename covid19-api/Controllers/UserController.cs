using covid19_api.Dtos.User;
using covid19_api.Services.SystemUser;
using Microsoft.AspNetCore.Mvc;

namespace covid19_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> GetAllUserss()
        {
            var data = await _userService.GetAllUsers();
            if(!data.Success)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }
    }
}
