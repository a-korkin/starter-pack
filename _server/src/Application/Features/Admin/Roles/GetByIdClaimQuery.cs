using System;
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
    public class GetByIdClaimQuery : IRequest<ClaimOutDto>
    {
        public Guid RoleId { get; set; }
        
        public Guid ClaimId { get; set; }

        public class GetByIdClaimQueryHandler : IRequestHandler<GetByIdClaimQuery, ClaimOutDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetByIdClaimQueryHandler(
                IApplicationDbContext context,
                IMapper mapper)
            {
                _context = context ??
                    throw new ArgumentNullException(nameof(context));

                _mapper = mapper ??
                    throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<ClaimOutDto> Handle(
                GetByIdClaimQuery request, 
                CancellationToken cancellationToken)
            {
                var claimEntity = await _context.Claims
                    .SingleOrDefaultAsync(w => w.RoleId == request.RoleId && w.Id == request.ClaimId);

                if (claimEntity == null)
                    throw new NotFoundException(name: typeof(Claim).FullName, key: request.ClaimId);

                return _mapper.Map<ClaimOutDto>(claimEntity);
            }
        }
    }
}