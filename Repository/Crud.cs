using Domain.Entities;
using Persistence;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void Create(TEntity item)
        {
            context.Manipulate<TEntity>().Add(item);
            context.SaveChanges();
        }

        public TEntity Delete(TPrimaryKey id)
        {
            var selectedItem = context.Manipulate<TEntity>().Where(x => x.ID.Equals(id)).FirstOrDefault();
            context.Manipulate<TEntity>().Remove(selectedItem);
            context.SaveChanges();
            return selectedItem;
        }

        public TEntity Read(TPrimaryKey id)
        {
            var selectedItem = context.Manipulate<TEntity>().Where(x => x.ID.Equals(id)).FirstOrDefault();
            return selectedItem;
        }

        public List<TEntity> SelectAll()
        {
            return context.Manipulate<TEntity>().ToList();
        }

        public TEntity Update(TEntity item)
        {
            context.Manipulate<TEntity>().Update(item);
            context.SaveChanges();
            return item;
        }
    }
}
