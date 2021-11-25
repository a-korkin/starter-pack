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
                // var metadata = (context.Resource as Microsoft.AspNetCore.Routing.RouteEndpoint).Metadata;
                // var x = (metadata as Microsoft.AspNetCore.Http.EndpointMetadataCollection)
                //     .Where(w => w.GetType().FullName == nameof(Microsoft.AspNetCore.Routing.HttpMethodMetadata))
                //     .Select(s => s as Microsoft.AspNetCore.Routing.HttpMethodMetadata);


                if (context.Resource is Microsoft.AspNetCore.Routing.RouteEndpoint resource
                    && resource.RoutePattern != null)
                {
                    var routePattern = resource.RoutePattern;
                    var controller = routePattern.RequiredValues["controller"].ToString();
                    var action = routePattern.RequiredValues["action"].ToString();
                    var userId = context.User.Claims.FirstOrDefault(c => c.Type == "id");
                }

                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}