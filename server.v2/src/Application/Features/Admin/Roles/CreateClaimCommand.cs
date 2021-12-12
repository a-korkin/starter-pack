using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models.DTO.Admin;
using AutoMapper;
using Domain.Entities.Admin;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Roles
{
    public class CreateClaimCommand : IRequest<ClaimOutDto>
    {
        public Guid RoleId { get; set; }
        public ClaimInDto ClaimIn { get; set; }

        public class CreateClaimCommandHandler : IRequestHandler<CreateClaimCommand, ClaimOutDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public CreateClaimCommandHandler(
                IApplicationDbContext context,
                IMapper mapper)
            {
                _context = context ??
                    throw new ArgumentNullException(nameof(context));

                _mapper = mapper ??
                    throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<ClaimOutDto> Handle(
                CreateClaimCommand request, 
                CancellationToken cancellationToken)
            {
                var roleExists = _context.Roles.AnyAsync(a => a.Id == request.RoleId);
                if (roleExists == null)
                    return null;
                
                var itemEntity = _mapper.Map<Claim>(request.ClaimIn);
                itemEntity.RoleId = request.RoleId;

                await _context.Claims.AddAsync(itemEntity);
                await _context.SaveChangesAsync();

                var itemToReturn = _mapper.Map<ClaimOutDto>(itemEntity);
                return itemToReturn;
            }
        }
    }
}