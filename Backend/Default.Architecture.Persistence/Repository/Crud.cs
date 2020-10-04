using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class Crud<TPrimaryKey, TEntity>
        where TPrimaryKey : IComparable, IConvertible, IComparable<TPrimaryKey>, IEquatable<TPrimaryKey>
        where TEntity : class, Identifiable<TPrimaryKey>
    {
        protected DbContext Context { get; private set; }

        public Crud(DbContext daoContext)
        {
            Context = daoContext;
        }

        public TEntity Create(TEntity item)
        {
            Context.Set<TEntity>().Add(item);
            Context.SaveChanges();
            return item;
        }

        public async Task<TEntity> CreateAsync(TEntity item) 
        {
            await Context.Set<TEntity>().AddAsync(item);
            await Context.SaveChangesAsync();
            return item;
        }

        public TEntity Delete(TPrimaryKey id)
        {
            var selectedItem = Read(id);
            Context.Set<TEntity>().Remove(selectedItem);
            Context.SaveChanges();
            return selectedItem;
        }

        public async Task<TEntity> DeleteAsync(TPrimaryKey id)
        {
            var selectedItem = await ReadAsync(id);
            Context.Set<TEntity>().Remove(selectedItem);
            await Context.SaveChangesAsync();
            return selectedItem;
        }

        public TEntity Read(TPrimaryKey id)
        {
            return Context.Set<TEntity>()
                .Where(x => x.Id.Equals(id))
                .FirstOrDefault();
        }

        public Task<TEntity> ReadAsync(TPrimaryKey id)
        {
            return Context.Set<TEntity>()
                .Where(x => x.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

        public TEntity Update(TEntity item)
        {
            Context.Set<TEntity>().Update(item);
            Context.SaveChanges();
            return item;
        }

        public async Task<TEntity> UpdateAsync(TEntity item)
        {
            Context.Set<TEntity>().Update(item);
            await Context.SaveChangesAsync();
            return item;
        }
    }
}
