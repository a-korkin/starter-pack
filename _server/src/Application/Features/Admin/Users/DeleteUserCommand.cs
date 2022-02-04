using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Users
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
        {
            private readonly IApplicationDbContext _context;

            public DeleteUserCommandHandler(IApplicationDbContext context)
            {
                _context = context ??
                    throw new ArgumentNullException(nameof(context));
            }

            public async Task<bool> Handle(
                DeleteUserCommand request, 
                CancellationToken cancellationToken)
            {
                var userEntity = await _context.Users.SingleOrDefaultAsync(w => w.Id == request.Id);
                
                if (userEntity != null)
                {
                    var result = _context.Users.Remove(userEntity);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
        }
    }
}