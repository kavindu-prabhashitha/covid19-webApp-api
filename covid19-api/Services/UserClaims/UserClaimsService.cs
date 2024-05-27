using System.Security.Claims;

namespace covid19_api.Services.UserClaims
{
    public class UserClaimsService : IUserClaimsService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserClaimsService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetUserName()
        {
            var result = string.Empty;
            if( _contextAccessor.HttpContext is not null ) {
                result = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;   
            }

            return result;
        }

        public string GetUserRole()
        {
            var result = string.Empty;
            if (_contextAccessor.HttpContext is not null)
            {
                result = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role) ?? string.Empty;
            }

            return result;
        }

        public string GetUserId()
        {
            var result = string.Empty;
            if (_contextAccessor.HttpContext is not null)
            {
                result = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            }

            return result;
        }
    }
}
