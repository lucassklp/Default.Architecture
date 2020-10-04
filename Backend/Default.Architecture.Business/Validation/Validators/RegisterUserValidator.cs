﻿using Domain.Dtos;
using Domain.Entities;
using FluentValidation;

namespace Default.Architecture.Business.Validation.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Invalid e-mail");
            
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
