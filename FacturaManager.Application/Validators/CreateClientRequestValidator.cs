using FacturaManager.Application.Contracts.Clients;
using FluentValidation;

namespace FacturaManager.Application.Validators
{
    public class CreateClientRequestValidator : AbstractValidator<CreateClientRequest>
    {
        public CreateClientRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Client name is required.")
                .MaximumLength(100);

            RuleFor(x => x.NIT)
                .NotEmpty().WithMessage("NIT is required.")
                .Length(14).WithMessage("NIT must be 14 characters.");

            RuleFor(x => x.DUI)
                .NotEmpty().WithMessage("DUI is required.")
                .Length(10).WithMessage("DUI must be 10 characters (e.g., 12345678-9).");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email format.")
                .When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.Phone)
                .MaximumLength(20)
                .When(x => !string.IsNullOrWhiteSpace(x.Phone));

            RuleFor(x => x.TenantId)
                .NotEmpty().WithMessage("Tenant ID is required.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("CreatedBy is required.");
        }
    }
}