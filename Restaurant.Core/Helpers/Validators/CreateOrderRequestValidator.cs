using FluentValidation;
using Restaurant.Data.Models.Order;

namespace Restaurant.Core.Helpers.Validators
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.OrderRequests).Must(x => x != null && (x.Count > 1));
            RuleForEach(x => x.OrderRequests).ChildRules(directors =>
            {
                directors.RuleFor(x => x.MenuId).NotEmpty();
                directors.RuleFor(x => x.Quantity).GreaterThan(0);
            });

        }
    }
}