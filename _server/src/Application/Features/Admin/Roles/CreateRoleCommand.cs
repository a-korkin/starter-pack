using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models.DTO.Admin;
using AutoMapper;
using Domain.Entities.Admin;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Roles
{
    public class CreateRoleCommand : IRequest<RoleOutDto>
    {
        public RoleInDto RoleIn { get; set; }

        public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleOutDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public CreateRoleCommandHandler(
                IApplicationDbContext context, 
                IMapper mapper)
            {
                _context = context ??
                    throw new ArgumentNullException(nameof(context));

                _mapper = mapper ??
                    throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<RoleOutDto> Handle(
                CreateRoleCommand request, 
                CancellationToken cancellationToken)
            {
                var role = _mapper.Map<Role>(request.RoleIn);
                await _context.Roles.AddAsync(role);

                await _context.SaveChangesAsync();
                role = await _context.Roles.FirstOrDefaultAsync(w => w.Id == role.Id);

                var entityToReturn = _mapper.Map<RoleOutDto>(role);
                return entityToReturn;
            }
        }
    }
}