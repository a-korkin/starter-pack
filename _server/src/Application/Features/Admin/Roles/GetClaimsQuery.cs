using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models.DTO.Admin;
using Application.Common.Models.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Roles
{
    public class GetClaimsQuery : ResourceParameters, IRequest<PaginatedList<ClaimOutDto>>
    {
        public Guid RoleId { get; set; }

        public class GetClaimsQueryHandler : IRequestHandler<GetClaimsQuery, PaginatedList<ClaimOutDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetClaimsQueryHandler(
                IApplicationDbContext context,
                IMapper mapper)
            {
                _context = context ??
                    throw new ArgumentNullException(nameof(context));

                _mapper = mapper ??
                    throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<PaginatedList<ClaimOutDto>> Handle(
                GetClaimsQuery request, 
                CancellationToken cancellationToken)
            {
                return await _context.Claims
                    .Where(w => w.RoleId == request.RoleId)
                    .ProjectTo<ClaimOutDto>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);
            }
        }
    }
}