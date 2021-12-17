using Application.Features.Admin.Users;
using FluentValidation;

namespace Application.Validations.Admin
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.UserIn.UserName).NotEmpty();
            RuleFor(x => x.UserIn.Password).NotEmpty();
            RuleFor(x => x.UserIn.ConfirmPassword).Equal(x => x.UserIn.Password);
        }
    }
}