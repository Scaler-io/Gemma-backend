using FluentValidation;
using Gemma.Order.Application.Models.Requests;

namespace Gemma.Order.Application.Validators
{
    public class PaymentDetailsValidator : AbstractValidator<PaymentDetailsRequest>
    {
        public PaymentDetailsValidator()
        {
            RuleFor(p => p.CardName)
                .NotEmpty().WithMessage("Card name is required");
            RuleFor(p => p.CardNumber)
                .NotEmpty().WithMessage("Card number is required");
            RuleFor(p => p.PaymentMethod)
                .NotEmpty().WithMessage("Payment method is required");
            RuleFor(p => p.CVV)
                .NotEmpty().WithMessage("CVV is required");
            RuleFor(p => p.Expiration)
                .NotEmpty().WithMessage("Expiration date is required");
        }
    }
}