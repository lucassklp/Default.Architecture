using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository
{
    public abstract class AbstractRepository<TEntity>
        where TEntity: class
    {
        protected DbContext Context { get; }
        public AbstractRepository(DbContext context)
        {
            this.Context = context;
        }

        protected DbSet<TEntity> Set()
        {
            return this.Context.Set<TEntity>();
        }
    }
}
