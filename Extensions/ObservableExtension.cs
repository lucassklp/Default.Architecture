using System;
using System.Reactive.Linq;

namespace Extensions
{
    public static class ObservableExtension
    {
        public static IObservable<TSource> ToActionResult<TSource>(this IObservable<TSource> observable)
        {
            return observable.Take(1);
        }
    }
}