using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models.DTO.Admin;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Roles
{
    public class GetAllClaimsQuery : IRequest<IEnumerable<ClaimOutDto>>
    {
        public Guid RoleId { get; set; }

        public class GetAllClaimsQueryHandler : IRequestHandler<GetAllClaimsQuery, IEnumerable<ClaimOutDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetAllClaimsQueryHandler(
                IApplicationDbContext context,
                IMapper mapper)
            {
                _context = context ??
                    throw new ArgumentNullException(nameof(context));

                _mapper = mapper ??
                    throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<IEnumerable<ClaimOutDto>> Handle(
                GetAllClaimsQuery request, 
                CancellationToken cancellationToken)
            {
                var claims = await _context.Claims
                    .Where(w => w.RoleId == request.RoleId)
                    .ToListAsync();
                
                return _mapper.Map<IEnumerable<ClaimOutDto>>(claims);
            }
        }
    }
}