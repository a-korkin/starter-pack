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

namespace Application.Features.Admin.Users
{
    public class GetUsersQuery : ResourceParameters, IRequest<PaginatedList<UserOutDto>>
    {
        public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginatedList<UserOutDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetUsersQueryHandler(
                IApplicationDbContext context,
                IMapper mapper)
            {
                _context = context ??
                    throw new ArgumentNullException(nameof(context));

                _mapper = mapper ??
                    throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<PaginatedList<UserOutDto>> Handle(
                GetUsersQuery request,
                CancellationToken cancellationToken)
            {
                var userList = await _context.Users
                    .ProjectTo<UserOutDto>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);

                return userList;
            }
        }
    }
}