using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models.DTO.Admin;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Roles
{
    public class GetByIdRoleQuery : IRequest<RoleOutItemDto>
    {
        public Guid Id { get; set; }

        public class GetByIdRoleQueryHandler : IRequestHandler<GetByIdRoleQuery, RoleOutItemDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetByIdRoleQueryHandler(
                IApplicationDbContext context, 
                IMapper mapper)
            {
                _context = context ??
                    throw new ArgumentNullException(nameof(context));

                _mapper = mapper ??
                    throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<RoleOutItemDto> Handle(
                GetByIdRoleQuery request, 
                CancellationToken cancellationToken)
            {
                var roleEntity = await _context.Roles
                    .SingleOrDefaultAsync(w => w.Id == request.Id);
                return _mapper.Map<RoleOutItemDto>(roleEntity);
            }
        }
    }
}