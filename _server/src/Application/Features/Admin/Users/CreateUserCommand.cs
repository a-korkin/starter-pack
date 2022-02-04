using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models.DTO.Admin;
using Domain.Entities.Admin;
using AutoMapper;
using MediatR;

namespace Application.Features.Admin.Users
{
    public class CreateUserCommand : IRequest<UserOutDto>
    {
        public UserInDto UserIn { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserOutDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public CreateUserCommandHandler(
                IApplicationDbContext context,
                IMapper mapper)
            {   
                _context = context ??
                    throw new ArgumentNullException(nameof(context));

                _mapper = mapper ??
                    throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<UserOutDto> Handle(
                CreateUserCommand request, 
                CancellationToken cancellationToken)
            {
                var userEntity = _mapper.Map<User>(request.UserIn);
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                userEntity.Password = BCrypt.Net.BCrypt.HashPassword(userEntity.Password, salt);

                await _context.Users.AddAsync(userEntity);
                await _context.SaveChangesAsync();

                return _mapper.Map<UserOutDto>(userEntity);
            }
        }
    }
}