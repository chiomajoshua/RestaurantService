using FluentValidation;
using Restaurant.Data.Models.Category;

namespace Restaurant.Core.Helpers.Validators
{
    public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().NotEqual("string");
            RuleFor(x => x.Description).NotEmpty().NotNull().NotEqual("string");
        }
    }
}