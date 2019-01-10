using FluentValidation;
using FluentValidation.Results;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Business
{
    public class ValidatorService
    {
        public IObservable<ValidationResult> CheckAsync<T>(IValidator<T> validator, T obj) where T: class
        {
            return Observable.Create<ValidationResult>(observer =>
            {
                var validation = validator.Validate(obj);
                if (!validation.IsValid)
                {
                    throw new ValidationException(validation.Errors);
                }
                observer.OnNext(validation);
                return Disposable.Empty;
            });
        }
    }
}
