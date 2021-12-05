using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models.DTO.Admin;
using AutoMapper;
using Domain.Entities.Admin;
using MediatR;

namespace Application.Features.UserFeatures.Commands
{
    public class CreateUserCommand : IRequest<UserOutDto>
    {
        // public UserInDto UserIn { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

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

            public async Task<UserOutDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var userIn = new UserInDto
                {
                    UserName = request.UserName,
                    Password = request.Password,
                    ConfirmPassword = request.ConfirmPassword
                };

                // var userEntity = _mapper.Map<User>(request.UserIn);
                var userEntity = _mapper.Map<User>(userIn);
                var userToReturn = _mapper.Map<UserOutDto>(await _context.Users.AddAsync(userEntity));

                return userToReturn;
            }
        }
    }
}