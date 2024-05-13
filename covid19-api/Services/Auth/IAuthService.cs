using covid19_api.Dtos.Auth;

namespace covid19_api.Services.Auth
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user,string password);
        Task<ServiceResponse<AuthTokenDto>> Login(string username,string password);
        Task<ServiceResponse<AuthTokenDto>> GenerateRefreshToken(AuthTokenDto token);
        Task<bool> UserExists(string username);
    }
}
