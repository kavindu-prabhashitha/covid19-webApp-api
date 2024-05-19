using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace covid19_api.Services
{
    public class ClaimsTransformationService : IClaimsTransformation
    {
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity?.IsAuthenticated != true)
            {
                return principal;
            }
            throw new NotImplementedException();
        }
    }
}
