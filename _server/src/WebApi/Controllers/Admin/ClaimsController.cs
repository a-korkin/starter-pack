using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Models.DTO.Admin;
using Application.Common.Models.Helpers;
using Application.Features.Admin.Roles;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Admin
{
    [Route("/api/admin/roles/{roleId}/claims")]
    public class ClaimsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClaimOutDto>>> GetAllAsync(
            [FromRoute] Guid roleId, 
            [FromQuery] GetClaimsQuery query) 
        {
            query.RoleId = roleId;
            var claims = await Mediator.Send(query);
            return Ok(claims);
        }

        [HttpGet("{claimId}", Name = "GetClaim")]
        public async Task<ActionResult<ClaimOutDto>> GetByIdAsync([FromRoute] GetByIdClaimQuery query)
        {
            var claim = await Mediator.Send(query);
            if (claim == null)
                return NotFound();

            return Ok(claim);
        }

        [HttpPost]
        public async Task<ActionResult<ClaimOutDto>> CreateAsync(
            [FromRoute] Guid roleId, 
            [FromBody] ClaimInDto item)
        {
            var claim = await Mediator.Send(new CreateClaimCommand { RoleId = roleId, ClaimIn = item });

            return CreatedAtRoute("GetClaim",
                new { roleId = roleId, claimId = claim.Id },
                claim);
        }

        [HttpPut("{claimId}")]
        public async Task<ActionResult<ClaimOutDto>> UpdateAsync(
            [FromRoute] Guid roleId,
            [FromRoute] Guid claimId, 
            [FromBody] ClaimUpdDto item)
        {
            var claim = await Mediator.Send(new UpdateClaimCommand { RoleId = roleId, ClaimId = claimId, ClaimUpd = item });
            if (claim == null)
                return NotFound();

            return Ok(claim);
        }
    }
}