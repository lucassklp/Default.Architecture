using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc.Authorization;
using Persistence;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Newtonsoft.Json.Serialization;
using DefaultArchitecture.Security.JwtSecurity;
using Microsoft.ApplicationInsights.Extensibility;

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

            //Disabling Telemery
            TelemetryConfiguration.Active.DisableTelemetry = true;

            //Configure the ConnectionString (Set the 'ConnectionString' section in appsettings.json)
            services.AddDbContext<DaoContext>(options => 
            {
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
            });

            //Injecting the services (See: Injector.cs)
            services.InjectServices();

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

            //Load the Jwt Configuration from the appsettings.json (See 'JwtConfiguration' section in appsettings.json)
            JwtTokenDefinitions.LoadFromConfiguration(Configuration);
            //Configuring Authentication
            services.ConfigureJwtAuthentication();
            //Configuring Authorization
            services.ConfigureJwtAuthorization();

            //All pages needs to be authenticated by default
            var mvc = services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                             .RequireAuthenticatedUser()
                             .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });


            mvc.AddJsonOptions(config =>
            {
                //All JSON returns lowerCamelCase (JSON Standard - By Google) instead of PascalCase (C# Standard - By Microsoft)
                //References: 
                //JSON Standards by Google https://google.github.io/styleguide/jsoncstyleguide.xml?showone=Property_Name_Format#Property_Name_Format
                //C# Standards by Microsoft https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/capitalization-conventions
                config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                //Trick for handling/ignoring Reference Loop Handling
                config.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            });

            //Add the Swagger for API documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "DefaultArchitecture API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //Configure logger for show debug info.
            loggerFactory.AddConsole(LogLevel.Debug);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DefaultArchitecture V1");
            });
            
            app.UseCors("policy");
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
