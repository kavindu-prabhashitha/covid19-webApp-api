using covid19_api.Services.Permission;
using covid19_api.Services.UserClaims;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace covid19_api.Handlers
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            //string? memberId =  context.User.Claims.FirstOrDefault(
            //     x => x.Type == JwtRegisteredClaimNames.Sub
            //     )?.Value;
            using IServiceScope scope02 = _serviceScopeFactory.CreateScope();
            IUserClaimsService userClaimsService = scope02.ServiceProvider
                .GetRequiredService<IUserClaimsService>();

            string? memberId = userClaimsService?.GetUserId();

            if (memberId is null)
            {
                return;
            }

            using IServiceScope scope = _serviceScopeFactory.CreateScope();

            IPermissionService permissionService = scope.ServiceProvider
                .GetRequiredService<IPermissionService>();

            List<int> permissions = await permissionService
                .GetPermissionsAsync(int.Parse(memberId));

            if (permissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }
        }
    }
}
