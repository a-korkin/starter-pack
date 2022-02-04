using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models.DTO.Admin;
using AutoMapper;
using Domain.Entities.Admin;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Roles
{
    public class UpdateClaimCommand : IRequest<ClaimOutDto>
    {
        public Guid RoleId { get; set; }

        public Guid ClaimId { get; set; }

        public ClaimUpdDto ClaimUpd { get; set; }

        public class UpdateClaimCommandHandler : IRequestHandler<UpdateClaimCommand, ClaimOutDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public UpdateClaimCommandHandler(
                IApplicationDbContext context,
                IMapper mapper)
            {
                _context = context ??
                    throw new ArgumentNullException(nameof(context));
                
                _mapper = mapper ??
                    throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<ClaimOutDto> Handle(
                UpdateClaimCommand request, 
                CancellationToken cancellationToken)
            {
                var claimEntity = await _context.Claims
                    .SingleOrDefaultAsync(w => w.Id == request.ClaimId);

                if (claimEntity == null)
                    throw new NotFoundException(name: typeof(Claim).FullName, key: request.ClaimId);
                    
                _mapper.Map(request.ClaimUpd, claimEntity);
                await _context.SaveChangesAsync();

                return _mapper.Map<ClaimOutDto>(claimEntity);
            }
        }
    }
}