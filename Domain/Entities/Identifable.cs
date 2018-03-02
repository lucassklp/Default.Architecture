using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public interface Identifiable<T>
        where T: IComparable, IConvertible, IComparable<T>, IEquatable<T>
    {
        T ID { get; set; }
    }
}
