

namespace covid19_api.Dtos.RefreshToken
{
    public class RevokeRefreshTokenRequestDto
    {
        public string UserName { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;
    }
}
