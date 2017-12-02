using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc.Authorization;
using Security.JwtSecurity;
using Persistence;
using Repository;
using Repository.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace DefaultArchitecture
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Configure logger
            services.AddLogging(builder =>
            {
                builder.AddConfiguration(Configuration.GetSection("Logging"))
                    .AddConsole()
                    .AddDebug();
            });

            TelemetryConfiguration.Active.DisableTelemetry = true;

            //Configure the ConnectionString
            services.AddDbContext<DaoContext>(options => 
            {
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
            });


            //Injecting the repositories
            services.AddTransient<IUserRepository, UserRepository>();

            //Configuring CORS
            services.AddCors(config =>
            {
                var policy = new CorsPolicy();
                policy.Headers.Add("*");
                policy.Methods.Add("*");
                policy.Origins.Add("*");
                policy.SupportsCredentials = true;
                config.AddPolicy("policy", policy);
            });

            //Configuring Authentication
            services.ConfigureJwtAuthentication();
            //Configuring Authorization
            services.ConfigureJwtAuthorization();
            

            //All pages needs to be authenticated
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                             .RequireAuthenticatedUser()
                             .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Debug);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
                
            app.UseCors("policy");
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
