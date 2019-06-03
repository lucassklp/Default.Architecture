using Core;
using FluentValidation;
using FluentValidation.Results;
using System;

namespace Business
{
    public class ValidatorService
    {
        public ValidationResult Check<T>(IValidator<T> validator, T obj)
        {
            var validation = validator.Validate(obj);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }
            return validation;
        }

        public IObservable<ValidationResult> CheckAsync<T>(IValidator<T> validator, T obj) where T : class
        {
            return SingleObservable.Create(() => Check(validator, obj));
        }
    }
}
