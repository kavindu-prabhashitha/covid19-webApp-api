using Microsoft.AspNetCore.Authorization;

namespace covid19_api.PermissionHandlers
{
    public class PermissionRequirement :IAuthorizationRequirement
    {
        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }

        public string Permission { get;  }
    }
}
