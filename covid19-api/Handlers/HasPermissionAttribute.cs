using covid19_api.Constants;
using Microsoft.AspNetCore.Authorization;

namespace covid19_api.Handlers
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(Permissions permission)
            : base(policy: ((int)permission).ToString()) { }

    }
}
