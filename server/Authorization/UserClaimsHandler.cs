using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace server.Authorization
{
    public class UserClaimsHandler : AuthorizationHandler<UserClaimsRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            UserClaimsRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.Gender))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}