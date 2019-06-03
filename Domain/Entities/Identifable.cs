using System;

namespace Domain.Entities
{
    public interface Identifiable<T>
        where T : IComparable, IConvertible, IComparable<T>, IEquatable<T>
    {
        T Id { get; set; }
    }
}
