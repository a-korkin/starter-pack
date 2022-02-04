using System;
using System.Collections.Generic;
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
    public class GetRolesQuery : ResourceParameters, IRequest<PaginatedList<RoleOutDto>>
    {
        public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, PaginatedList<RoleOutDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetRolesQueryHandler(
                IApplicationDbContext context,
                IMapper mapper)
            {
                _context = context ??
                    throw new ArgumentNullException(nameof(context));

                _mapper = mapper ??
                    throw new ArgumentNullException(nameof(mapper));                
            }

            public async Task<PaginatedList<RoleOutDto>> Handle(
                GetRolesQuery request, 
                CancellationToken cancellationToken)
            {
                return await _context.Roles
                    .ProjectTo<RoleOutDto>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);
            }
        }
    }
}