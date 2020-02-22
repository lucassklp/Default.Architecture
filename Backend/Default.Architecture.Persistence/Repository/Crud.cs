using Default.Architecture.Core;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace Persistence.Repository
{
    public class Crud<TPrimaryKey, TEntity>
        where TPrimaryKey : IComparable, IConvertible, IComparable<TPrimaryKey>, IEquatable<TPrimaryKey>
        where TEntity : class, Identifiable<TPrimaryKey>
    {
        private DbContext context;

        public Crud(DbContext daoContext)
        {
            context = daoContext;
        }

        public TEntity Create(TEntity item)
        {
            context.Set<TEntity>().Add(item);
            context.SaveChanges();
            return item;
        }

        public IObservable<TEntity> CreateAsync(TEntity item) => SingleObservable.Create(() => Create(item));

        public TEntity Delete(TPrimaryKey id)
        {
            var selectedItem = Read(id);
            context.Set<TEntity>().Remove(selectedItem);
            context.SaveChanges();
            return selectedItem;
        }

        public IObservable<TEntity> DeleteAsync(TPrimaryKey id) => SingleObservable.Create(() => Delete(id));

        public TEntity Read(TPrimaryKey id)
        {
            return context.Set<TEntity>()
                .Where(x => x.Id.Equals(id))
                .FirstOrDefault();
        }

        public IObservable<TEntity> ReadAsync(TPrimaryKey id) => SingleObservable.Create(() => Read(id));

        public TEntity Update(TEntity item)
        {
            context.Set<TEntity>().Update(item);
            context.SaveChanges();
            return item;
        }

        public IObservable<TEntity> UpdateAsync(TEntity item) => SingleObservable.Create(() => Update(item));
    }
}
