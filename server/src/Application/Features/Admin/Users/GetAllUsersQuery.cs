using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models.DTO.Admin;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Users
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserOutDto>>
    {
        public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserOutDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetAllUsersQueryHandler(
                IApplicationDbContext context,
                IMapper mapper)
            {
                _context = context ??
                    throw new ArgumentNullException(nameof(context));

                _mapper = mapper ??
                    throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<IEnumerable<UserOutDto>> Handle(
                GetAllUsersQuery request,
                CancellationToken cancellationToken)
            {
                var userList = await _context.Users.ToListAsync();
                return _mapper.Map<IEnumerable<UserOutDto>>(userList);
            }
        }
    }
}