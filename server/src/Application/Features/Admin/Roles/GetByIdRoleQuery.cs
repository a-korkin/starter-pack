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
    public class GetByIdRoleQuery : IRequest<RoleOutDto>
    {
        public Guid Id { get; set; }

        public class GetByIdRoleQueryHandler : IRequestHandler<GetByIdRoleQuery, RoleOutDto>
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

            public async Task<RoleOutDto> Handle(
                GetByIdRoleQuery request, 
                CancellationToken cancellationToken)
            {
                var roleEntity = await _context.Roles
                    .SingleOrDefaultAsync(w => w.Id == request.Id);

                if (roleEntity == null)
                    throw new NotFoundException(name: typeof(Role).FullName, key: request.Id);
                    
                return _mapper.Map<RoleOutDto>(roleEntity);
            }
        }
    }
}