using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Authorization
{
    public class UserClaimsHandler : AuthorizationHandler<UserClaimsRequirement>
    {
        private readonly string CREATE_ACTION = "create";
        private readonly string[] GET_ACTIONS = new string[]
        {
            "getall", "getbyid"
        };
        private readonly string UPDATE_ACTION = "update";
        private readonly string DELETE_ACTION = "delete";
        private readonly IApplicationDbContext _context;

        public UserClaimsHandler(IApplicationDbContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        private async Task<IEnumerable<Claim>> GetClaims(Guid userId)
        {
            var claims = await _context.Users
                .Where(w => w.Id == userId)
                .Include(i => i.Roles)
                .ThenInclude(i => i.Role)
                .ThenInclude(i => i.Claims)
                .ThenInclude(i => i.Type)
                .SelectMany(s => s.Roles.SelectMany(a => a.Role.Claims))
                .ToListAsync();

            return claims;
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
                        var claims = Task.Run(async () => await GetClaims(userId)).Result;

                        bool canCreate = CREATE_ACTION == action && claims.Any(a => a.Type.Schema == scheme && a.Type.Slug == controller && a.Create);
                        bool canRead = GET_ACTIONS.Contains(action) && claims.Any(a => a.Type.Schema == scheme && a.Type.Slug == controller && a.Read);
                        bool canUpdate = UPDATE_ACTION == action && claims.Any(a => a.Type.Schema == scheme && a.Type.Slug == controller && a.Update);
                        bool canDelete = DELETE_ACTION == action && claims.Any(a => a.Type.Schema == scheme && a.Type.Slug == controller && a.Delete);

                        if (canCreate || canRead || canUpdate || canDelete)
                            context.Succeed(requirement);
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}