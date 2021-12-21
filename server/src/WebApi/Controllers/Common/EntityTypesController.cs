using System;
using System.Threading.Tasks;
using Application.Common.Models.DTO.Common;
using Application.Common.Models.Helpers;
using Application.Features.Common.EntityTypes;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Common
{
    [Route("/api/common/entity-types")]
    public class EntityTypesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<EntityTypeOutDto>>> GetAllAsync(
            [FromQuery] GetEntityTypesQuery query)
        {
            var entityTypes = await Mediator.Send(query);
            return Ok(entityTypes);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync()
        {
            var entityTypes = await Mediator.Send(new RefreshEntityTypesCommand());
            return Ok(entityTypes);
        }
    }
}