using Microsoft.EntityFrameworkCore;
using System;

namespace Persistence.Repository
{
    public class TransactionScope
    {
        private DbContext context;
        public TransactionScope(DbContext context)
        {
            this.context = context;
        }

        public void Create(Action actions)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    actions();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }

        }
    }
}
