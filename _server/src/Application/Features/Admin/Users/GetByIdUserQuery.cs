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

namespace Application.Features.Admin.Users
{
    public class GetByIdUserQuery : IRequest<UserOutDto>
    {
        public Guid Id { get; set; }

        public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, UserOutDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetByIdUserQueryHandler(
                IApplicationDbContext context,
                IMapper mapper)
            {
                _context = context ??
                    throw new ArgumentNullException(nameof(context));

                _mapper = mapper ??
                    throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<UserOutDto> Handle(
                GetByIdUserQuery request, 
                CancellationToken cancellationToken)
            {
                var userEntity = await _context.Users
                    .SingleOrDefaultAsync(w => w.Id == request.Id);

                if (userEntity == null)
                    throw new NotFoundException(name: typeof(User).FullName, key: request.Id);
                
                return _mapper.Map<UserOutDto>(userEntity);
            }
        }
    }
}