using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using server.Entities.Admin;
using server.Repositories;

namespace server.Authorization
{
    public class UserClaimsHandler : AuthorizationHandler<UserClaimsRequirement>
    {
        private readonly IUserRepository _repository;

        public UserClaimsHandler(IUserRepository repository)
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
                        string scheme = String.Empty;
                        if (routePattern.PathSegments.Any() &&
                            routePattern.PathSegments[1].Parts.FirstOrDefault() is Microsoft.AspNetCore.Routing.Patterns.RoutePatternLiteralPart _scheme)
                        {
                            scheme = _scheme.Content.ToLower();
                        }

                        var controller = routePattern.RequiredValues["controller"].ToString().ToLower();
                        var action = routePattern.RequiredValues["action"].ToString().ToLower();
                        var userId = Guid.Parse(context.User.Claims.FirstOrDefault(c => c.Type == "id").Value);
                        var claims = _repository.GetUserClaimsAsync(userId).Result;

                        
                    }
                }

                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}