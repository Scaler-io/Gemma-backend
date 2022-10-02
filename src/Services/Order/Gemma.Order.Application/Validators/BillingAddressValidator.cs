using FluentValidation;
using Gemma.Order.Application.Models.Requests;

namespace Gemma.Order.Application.Validators
{
    public class BillingAddressValidator : AbstractValidator<BillingAddressRequest>
    {
        public BillingAddressValidator()
        {
            RuleFor(b => b.FirstName)
                .NotEmpty().WithMessage("First name is required");
            RuleFor(b => b.LastName)
                .NotEmpty().WithMessage("Last name is required");
            RuleFor(b => b.AddressLine)
                .NotEmpty().WithMessage("Address line is required");
            RuleFor(b => b.Country)
                .NotEmpty().WithMessage("Country is required");
            RuleFor(o => o.EmailAddress)
                    .NotEmpty().WithMessage("Email address is required");
        }
    }
}