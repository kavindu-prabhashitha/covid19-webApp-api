using covid19_api.Dtos.Auth;
using covid19_api.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace covid19_api.Services.Auth
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user,string password);
        Task<ServiceResponse<int>> RegisterAdminUser(User user, string password);
        Task<ServiceResponse<UserLoginResponseDto>> Login(string username,string password);
        Task<ServiceResponse<AuthTokenDto>> GenerateRefreshToken(AuthTokenDto token);

        Task<bool> UserExists(string username);
    }
}
