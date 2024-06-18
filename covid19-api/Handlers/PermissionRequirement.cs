using Microsoft.AspNetCore.Authorization;

namespace covid19_api.Handlers
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(int permission)
        {
            Permission = permission;
        }

        public int Permission { get; }
    }
}
