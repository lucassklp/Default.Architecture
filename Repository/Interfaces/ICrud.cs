using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface ICrud<TPrimaryKey, TEntity>
        where TPrimaryKey : IComparable, IConvertible, IComparable<TPrimaryKey>, IEquatable<TPrimaryKey>
        where TEntity : class, Identifiable<TPrimaryKey>
    {
        void Create(TEntity item);
        TEntity Delete(TPrimaryKey id);
        TEntity Read(TPrimaryKey id);
        List<TEntity> SelectAll();
        TEntity Update(TEntity item);
    }
}
