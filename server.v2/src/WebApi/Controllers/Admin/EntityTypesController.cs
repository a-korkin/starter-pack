using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Application.Features.Common.EntityTypes;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Admin
{
    [Route("/api/admin/entity-types")]
    public class EntityTypesController : ApiControllerBase
    {
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync()
        {
            var entityTypes = await Mediator.Send(new RefreshEntityTypesCommand());
            return Ok(entityTypes);
        }
    }
}