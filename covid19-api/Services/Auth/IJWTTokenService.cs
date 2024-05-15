using covid19_api.Dtos.Auth;
using System.Security.Claims;

namespace covid19_api.Services.Auth
{
    public interface IJWTTokenService
    {
        AuthTokenDto GenerateJWTTokens(User user);

        AuthTokenDto GenerateRefreshToken(User user);

        Task<UserRefreshToken> AddUserRefreshTokens(UserRefreshToken user);

        Task<ServiceResponse<string>> DeleteUserRefreshTokens(string username, string refreshToken);

        UserRefreshToken GetSavedRefreshTokens(string username, string refreshToken);

        Task<bool> IsValidUserAsync(User user);

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
