using Domain.Entities;
using Persistence;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Repository
{
    public class Crud<TPrimaryKey, TEntity> : ICrud<TPrimaryKey, TEntity>
        where TPrimaryKey: IComparable, IConvertible, IComparable<TPrimaryKey>, IEquatable<TPrimaryKey>
        where TEntity : class, Identifiable<TPrimaryKey>
    {
        private DaoContext context;

        public Crud(DaoContext daoContext)
        {
            context = daoContext;
        }

        public IObservable<TEntity> Create(TEntity item)
        {
            return Observable.Create<TEntity>(observer =>
            {
                context.Manipulate<TEntity>().Add(item);
                context.SaveChanges();
                observer.OnNext(item);
                return Disposable.Empty;
            });
        }

        public IObservable<TEntity> Delete(TPrimaryKey id)
        {
            return Observable.Create<TEntity>(observer =>
            {
                var selectedItem = context.Manipulate<TEntity>().Where(x => x.ID.Equals(id)).FirstOrDefault();
                context.Manipulate<TEntity>().Remove(selectedItem);
                context.SaveChanges();
                observer.OnNext(selectedItem);
                return Disposable.Empty;
            });
        }

        public IObservable<TEntity> Read(TPrimaryKey id)
        {
            return Observable.Create<TEntity>(observer =>
            {
                observer.OnNext(context.Manipulate<TEntity>().Where(x => x.ID.Equals(id)).FirstOrDefault());
                return Disposable.Empty;
            });
        }

        public IObservable<List<TEntity>> Paged(int page, int pageSize)
        {
            return Observable.Create<List<TEntity>>(observer =>
            {
                var items = context.Manipulate<TEntity>()
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
                observer.OnNext(items);
                return Disposable.Empty;
            });
        }

        public IObservable<TEntity> Update(TEntity item)
        {
            return Observable.Create<TEntity>(observer =>
            {
                context.Manipulate<TEntity>().Update(item);
                context.SaveChanges();
                observer.OnNext(item);
                return Disposable.Empty;
            });
        }
    }
}
