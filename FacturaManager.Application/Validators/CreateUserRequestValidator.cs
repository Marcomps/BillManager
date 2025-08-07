using FluentValidation;
using FacturaManager.Application.Contracts.Users;

namespace FacturaManager.Application.Validators;
public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Username).NotEmpty().MinimumLength(4);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        RuleFor(x => x.RoleId).NotEmpty();
    }
}