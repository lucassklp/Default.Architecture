using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence
{
    public static class Injector
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<DaoContext>();
            services.AddTransient<DbContext, DaoContext>();

            //Migrate Database
            var daoContext = services.BuildServiceProvider().GetService<DaoContext>();
            daoContext.Database.Migrate();

            return services;
        }
    }
}
