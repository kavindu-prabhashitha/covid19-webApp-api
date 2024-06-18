using covid19_api.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace covid19_api.Providers
{
    public class PermissionAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public PermissionAuthorizationPolicyProvider(
            IOptions<AuthorizationOptions> options)
            : base(options)
        {
        }

        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            AuthorizationPolicy? policy = await base.GetPolicyAsync(policyName);

            if (policy is not null)
            {
                return policy;
            }

            return new AuthorizationPolicyBuilder()
                .AddRequirements(new PermissionRequirement(Int32.Parse(policyName)))
                .Build();
        }
    }
}
