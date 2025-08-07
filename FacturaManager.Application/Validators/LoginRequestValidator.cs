using FluentValidation;
using FacturaManager.Application.Contracts.Auth;

namespace FacturaManager.Application.Validators;
public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}