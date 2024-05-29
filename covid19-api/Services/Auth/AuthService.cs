
using AutoMapper;
using Azure;
using Azure.Core;
using covid19_api.Dtos.Auth;
using covid19_api.Dtos.User;
using covid19_api.Dtos.UserRole;
using covid19_api.Services.JwtToken;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace covid19_api.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IJWTTokenService _jwtTokenService;
        private readonly IMapper _mapper;

        public AuthService(
            DataContext context, 
            IConfiguration configuration,
            IJWTTokenService jwtTokenService,
            IMapper mapper) {
            _configuration = configuration;
            _context = context;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<UserLoginResponseDto>> Login(string username, string password)
        {
            var response = new ServiceResponse<UserLoginResponseDto>();
            var user = await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()));
            if (user is null)
            {
                response.Success = false;
                response.Message = "User not Found";
                return response;
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong Password or Username";
                return response;
            }
            else
            {
                var jwtToken = _jwtTokenService?.GenerateJWTTokens(user);

                if(jwtToken == null)
                {
                    response.Success = false;
                    response.Message = "Jwt Token Not Generated";
                    return response;
                }
                var loginRes = new UserLoginResponseDto()
                {
                    UserName = username,
                    AccessToken = jwtToken.AccessToken,
                    RefreshToken = jwtToken.RefreshToken,
                };
                response.Data = loginRes;
            }

            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var serviceResponse = new ServiceResponse<int>();
            if (await UserExists(user.Username))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User Already exists";
                return serviceResponse;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            //Get UserRole from the database
            var userRole = await _context.UserRoles
                .Where(x => x.Id == 2)
                .FirstOrDefaultAsync();

            user.Role = userRole;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var response = new ServiceResponse<int>();
            response.Data = user.Id;
            response.Message = "User Created Successfully";
            return response;
        }


        public async Task<ServiceResponse<int>> RegisterAdminUser(User user, string password)
        {
            var serviceResponse = new ServiceResponse<int>();
            if (await UserExists(user.Username))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Admin User Already exists";
                return serviceResponse;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            //Get Administrator role from db
            var adminRole = await _context.UserRoles
                .Where(x => x.Id == 1)
                .FirstOrDefaultAsync();

            user.Role = adminRole;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var response = new ServiceResponse<int>();
            response.Data = user.Id;
            response.Message = "Admin Created Successfully";
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(u => u.Username.ToLower().Equals(username.ToLower())))
            {
                return true;
            }
            return false;
        }

        public async Task<ServiceResponse<AuthTokenDto>> GenerateRefreshToken(AuthTokenDto token)
        {
            var serviceResponse = new ServiceResponse<AuthTokenDto>();
            var principal = _jwtTokenService.GetPrincipalFromExpiredToken(token.AccessToken);
            var username = principal.Identity?.Name;

            var user = await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()));
            if (user is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User not Found";
            }


            var savedRefreshToken = _jwtTokenService.GetSavedRefreshTokens(username, token.RefreshToken);

            if (savedRefreshToken.RefreshToken != token.RefreshToken)
            {
                serviceResponse.Message = "Invalid attempt!";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            var newJwtToken = _jwtTokenService.GenerateRefreshToken(user);

            if (newJwtToken == null)
            {
                serviceResponse.Message = "Invalid attempt!";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            UserRefreshToken obj = new UserRefreshToken
            {
                RefreshToken = newJwtToken.RefreshToken,
                UserName = username
            };

            await _jwtTokenService.DeleteUserRefreshTokens(username, token.RefreshToken);
            await _jwtTokenService.AddUserRefreshTokens(obj);

            serviceResponse.Data = newJwtToken;

            return serviceResponse;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;

            if (appSettingsToken is null)
            {
                throw new Exception("AppSetting Token is Null");
            }

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(appSettingsToken));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);


            var tokenCreated = tokenHandler.WriteToken(token);
            return tokenCreated;

        }

        public async Task<ServiceResponse<GetUserDto>> UpdateUserRole(UpdateUserRoleDto data)
        {
            var response = new ServiceResponse<GetUserDto>();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == data.UserId);  
            if (user is null)
            {
                response.Success = false;
                response.Message = "User Does not exist";
                return response;
            }

            var newRole = await _context.UserRoles.FirstOrDefaultAsync(x => x.Id == data.RoleId);

            if (newRole is null)
            {
                response.Success = false;
                response.Message = "Provided Role Not Found";
                return response;
            }

            user.Role = newRole;
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<GetUserDto>(user);

            return response;
        }
    }

}

