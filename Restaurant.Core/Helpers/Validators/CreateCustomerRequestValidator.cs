using FluentValidation;
using Restaurant.Data.Models.Customer;

namespace Restaurant.Core.Helpers.Validators
{
    public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
    {
        public CreateCustomerRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().NotNull().NotEqual("string");
            RuleFor(x => x.LastName).NotEmpty().NotNull().NotEqual("string");
            RuleFor(x => x.EmailAddress).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().NotEqual("string");
            RuleFor(x => x.Password).NotEmpty().NotNull().NotEqual("string");
        }
    }
}