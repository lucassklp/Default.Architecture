using FluentValidation;
using System;
using System.Reactive.Linq;

namespace Business
{
    public class ValidatorService
    {
        public IObservable<IValidator<T>> Check<T>(IValidator<T> validator) where T: class
        {
            return Observable.ToAsync(delegate (IValidator<T> v) { return v; })(validator);
        }
    }
}
