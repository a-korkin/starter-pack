using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using server.Entities.Admin;
using server.Services;

namespace server.Authorization
{
    public class UserClaimsHandler : AuthorizationHandler<UserClaimsRequirement>
    {
        private readonly IBaseRepository<User> _repository;

        public UserClaimsHandler(IBaseRepository<User> repository)
        {
            _repository = repository ?? 
                throw new ArgumentNullException(nameof(repository));
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            UserClaimsRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "id"))
            {
                if (context.Resource is Microsoft.AspNetCore.Routing.RouteEndpoint resource
                    && resource.RoutePattern != null)
                {
                    var routePattern = resource.RoutePattern;
                    if (routePattern != null) 
                    {
                        var controller = routePattern.RequiredValues["controller"].ToString().ToLower();
                        var action = routePattern.RequiredValues["action"].ToString().ToLower();
                        var userId = Guid.Parse(context.User.Claims.FirstOrDefault(c => c.Type == "id").Value);
                        var user = _repository.GetByIdAsync(userId).Result;
                        var userClaims = user.UserClaims;
                        foreach (var claim in userClaims)
                        {
                            Console.WriteLine(claim.Claim);
                        }
                    }
                }

                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}