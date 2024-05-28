using Azure.Core;
using covid19_api.Dtos.Auth;
using covid19_api.Dtos.RefreshToken;
using covid19_api.Dtos.User;
using covid19_api.Dtos.UserRole;
using covid19_api.PermissionHandlers;
using covid19_api.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace covid19_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJWTTokenService _jwtTokenService;
        public AuthController(IAuthService authService, IJWTTokenService jwtTokenService)
        {
            _authService = authService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> RegisterUser(UserRegisterDto request)
        {
            var response = await _authService.Register(
                new User { Username = request.Username }, request.Password
            );

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Register-Admin")]
        [Authorize(Roles ="ADMINISTRATOR")]
        public async Task<ActionResult<ServiceResponse<int>>> RegisterAdminUser(UserRegisterDto data)
        {
            var response = await _authService.RegisterAdminUser(
            new User { Username = data.Username }, data.Password
            );

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<UserLoginResponseDto>>> Login(UserLoginDto request)
        {
            var response = await _authService.Login(
                request.Username, request.Password
            );
            
            if (response.Data == null) {
                return Unauthorized("Invalid Attempt..");
            }

            UserRefreshToken obj = new UserRefreshToken();
            obj.RefreshToken = response.Data.RefreshToken;
            obj.UserName = request.Username;

            await _jwtTokenService.AddUserRefreshTokens(obj);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<ServiceResponse<AuthTokenDto>>> Refresh(AuthTokenDto token)
        {
       
            var response = await _authService.GenerateRefreshToken(token);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("revoke-refresh-token")]
        public async Task<ActionResult<ServiceResponse<string>>> RevokeRefreshToken(RevokeRefreshTokenRequestDto data)
        {

            var response = await _jwtTokenService.DeleteUserRefreshTokens(data.UserName, data.RefreshToken);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("UpdateUserRole")]
        [HasPermission(Permissions.UPDATE_ROLE)]    
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateUserRole(UpdateUserRoleDto data)
        {
            var response = await _authService.UpdateUserRole(data);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
