using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Models.DTO.Admin;
using Application.Features.Admin.Roles;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Admin
{
    [Route("/api/admin/roles")]
    public class RolesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleOutDto>>> GetAllAsync([FromQuery] GetRolesQuery query)
        {
            var roles = await Mediator.Send(query);
            return Ok(roles);
        }

        [HttpGet("{id}", Name = "GetRole")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] GetByIdRoleQuery query)
        {
            var entityToReturn = await Mediator.Send(query);

            if (entityToReturn != null)
                return Ok(entityToReturn);
            
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] RoleInDto item)
        {
            var entityToReturn = await Mediator.Send(new CreateRoleCommand { RoleIn = item });

            return CreatedAtRoute("GetRole",
                new { id = entityToReturn.Id },
                entityToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] DeleteRoleCommand command)
        {
            if (await Mediator.Send(command))
                return NoContent();
            
            return NotFound();
        }
    }
}