using Domain.Entities;
using FluentValidation;

namespace Business.Validation.Validators
{
    public class RegisterUserValidator : AbstractValidator<User>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Invalid E-mail");
            
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("The username cannot be null or empty");
            
            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("The password cannot be null/empty and must have at least 6 characters");
        }
    }
}
