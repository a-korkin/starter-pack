using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models.DTO.Admin;
using AutoMapper;
using Domain.Attributes;
using Domain.Entities.Admin;
using Domain.Entities.Base;
using Domain.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserFeatures.Commands
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

            public async Task<UserOutDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var userEntity = _mapper.Map<User>(request.UserIn);

                DescriptionAttribute attribute =
                    (DescriptionAttribute)Attribute.GetCustomAttribute(typeof(User), typeof(DescriptionAttribute));

                if (attribute != null) 
                {
                    var entityType = await _context.EntityTypes
                        .Where(w => w.Schema == attribute.Schema)                    
                        .Where(w => w.TableName == attribute.TableName)
                        .FirstOrDefaultAsync();

                    var entity = new Entity 
                    {
                        Id = userEntity.Id,
                        Type = entityType
                    };
                    await _context.Entities.AddAsync(entity);
                }

                await _context.Users.AddAsync(userEntity);
                await _context.SaveChangesAsync();

                return _mapper.Map<UserOutDto>(userEntity);
            }
        }
    }
}