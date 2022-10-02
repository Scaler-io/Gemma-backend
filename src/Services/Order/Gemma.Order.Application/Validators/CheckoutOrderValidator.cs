using FluentValidation;
using Gemma.Order.Application.Models.Requests.Checkout;

namespace Gemma.Order.Application.Validators
{
    public class CheckoutOrderValidator: AbstractValidator<CheckoutOrderRequest>
    {
        public CheckoutOrderValidator()
        {
            RuleFor(p => p.UserName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{UserName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters");

            RuleFor(p => p.TotalPrice)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{TotalPrice} is required")
                .GreaterThan(0).WithMessage("Total price should be greater than 0");

            RuleFor(p => p.Address)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Address can not be empty")
                .SetValidator(new BillingAddressValidator());
            
            RuleFor(p => p.PaymentDetails)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Paymentdetails can not be empty")
                .SetValidator(new PaymentDetailsValidator());
        }
    }
}
