using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models.DTO.Admin;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Roles
{
    public class GetAllRolesQuery : IRequest<IEnumerable<RoleOutDto>>
    {
        public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<RoleOutDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetAllRolesQueryHandler(
                IApplicationDbContext context,
                IMapper mapper)
            {
                _context = context ??
                    throw new ArgumentNullException(nameof(context));

                _mapper = mapper ??
                    throw new ArgumentNullException(nameof(mapper));                
            }

            public async Task<IEnumerable<RoleOutDto>> Handle(
                GetAllRolesQuery request, 
                CancellationToken cancellationToken)
            {
                var roleList = await _context.Roles.ToListAsync();
                return _mapper.Map<IEnumerable<RoleOutDto>>(roleList);
            }
        }
    }
}