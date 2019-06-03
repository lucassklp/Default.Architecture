using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public abstract class AbstractRepository
    {
        protected DbContext Context { get; }
        public AbstractRepository(DbContext context)
        {
            this.Context = context;
        }

        protected DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return this.Context.Set<TEntity>();
        }
    }
}
