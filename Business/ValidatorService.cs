using FluentValidation;
using FluentValidation.Results;
using System;
using System.Reactive.Linq;

namespace Business
{
    public class ValidatorService
    {
        public IObservable<ValidationResult> CheckAsync<T>(IValidator<T> validator, T obj) where T: class
        {
            return Observable.ToAsync<IValidator<T>, T, ValidationResult>(this.Check)(validator, obj);
        }

        public ValidationResult Check<T>(IValidator<T> validator, T entity) where T : class
        {
            var validation = validator.Validate(entity);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }
            return validation;
        }
    }
}
