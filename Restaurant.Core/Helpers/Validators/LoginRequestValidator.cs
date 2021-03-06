using FluentValidation;
using Restaurant.Data.Models.Authentication;

namespace Restaurant.Core.Helpers.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().NotNull().NotEqual("string");
        }
    }
}