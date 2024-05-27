using Microsoft.AspNetCore.Authorization;

namespace covid19_api.PermissionHandlers
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(Permissions permission ) 
            : base( policy : permission.ToString() ) { }
       
    }
}
