using FluentValidation;
using Restaurant.Data.Models.Menu;

namespace Restaurant.Core.Helpers.Validators
{
    public class CreateMenuRequestValidator : AbstractValidator<CreateMenuRequest>
    {
        public CreateMenuRequestValidator()
        {
            RuleFor(x => x.DishName).NotEmpty().NotNull().NotEqual("string");
            RuleFor(x => x.Description).NotEmpty().NotNull().NotEqual("string");
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.CategoryId).NotEmpty();
        }
    }
}