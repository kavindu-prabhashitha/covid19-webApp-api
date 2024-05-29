using covid19_api.Constants;
using covid19_api.Dtos.Auth;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace covid19_api.Services.JwtToken
{
    public class JWTTokenService : IJWTTokenService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public JWTTokenService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public AuthTokenDto GenerateRefreshToken(User user)
        {
            return GenerateJWTTokens(user);
        }

        public AuthTokenDto GenerateJWTTokens(User user)
        {
            try
            {

                var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;

                if (appSettingsToken is null)
                {
                    throw new Exception("AppSetting Token is Null");
                }

                var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role!.Uid)

                    };
                var tokenKey = Encoding.UTF8.GetBytes(appSettingsToken);
                SigningCredentials creds = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);


                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddMinutes(15),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var refreshToken = GenerateRefreshToken();
                return new AuthTokenDto
                {
                    AccessToken = tokenHandler.WriteToken(token),
                    RefreshToken = refreshToken
                };
            }
            catch (Exception ex)
            {
                return new AuthTokenDto();
            }
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task<UserRefreshToken> AddUserRefreshTokens(UserRefreshToken user)
        {

            _context.UserRefreshTokens.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<ServiceResponse<string>> DeleteUserRefreshTokens(string username, string refreshToken)
        {
            var response = new ServiceResponse<string>();
            var item = _context.UserRefreshTokens.FirstOrDefault(x => x.UserName == username && x.RefreshToken == refreshToken);
            if (item != null)
            {
                _context.UserRefreshTokens.Remove(item);
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Data = "Refresh token Deleted";
                return response;

            }

            response.Success = false;
            response.Message = "Something went wrong";
            return response;

        }

        public UserRefreshToken GetSavedRefreshTokens(string username, string refreshToken)
        {
            return _context.UserRefreshTokens.FirstOrDefault(x => x.UserName == username && x.RefreshToken == refreshToken && x.IsActive == true);
        }

        public async Task<bool> IsValidUserAsync(User user)
        {
            var u = _context.Users.FirstOrDefault(o => o.Username == user.Username);

            if (u != null)
                return true;
            else
                return false;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;
            if (appSettingsToken is null)
            {
                throw new Exception("AppSetting Token is Null");
            }
            var Key = Encoding.UTF8.GetBytes(appSettingsToken);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        private string getUserRole(UserRoles userRole)
        {
            var uRole = "0";
            switch (userRole)
            {
                case UserRoles.ADMINISTRATOR:
                    uRole = "ADMINISTRATOR";
                    break;
                case UserRoles.USER:
                    uRole = "USER";
                    break;

            }
            return uRole;
        }

    }
}
