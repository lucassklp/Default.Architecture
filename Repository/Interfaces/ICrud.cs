using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface ICrud<TPrimaryKey, TEntity>
        where TPrimaryKey : IComparable, IConvertible, IComparable<TPrimaryKey>, IEquatable<TPrimaryKey>
        where TEntity : class, Identifiable<TPrimaryKey>
    {
        IObservable<TEntity> Create(TEntity item);
        IObservable<TEntity> Delete(TPrimaryKey id);
        IObservable<TEntity> Read(TPrimaryKey id);
        IObservable<List<TEntity>> Paged(int page, int pageSize);
        IObservable<TEntity> Update(TEntity item);
    }
}
