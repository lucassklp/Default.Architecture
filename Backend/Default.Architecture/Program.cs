using Default.Architecture;
using Default.Architecture.Authentication.Jwt;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(config =>
{
    config.AddPolicy("DevelopmentCorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.EnableJwtAuthentication(builder.Configuration);
builder.Services.EnableJwtAuthorization();

//All endpoints requires an authenticated user
var mvc = builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
                 .RequireAuthenticatedUser()
                 .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

mvc.AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

mvc.AddNewtonsoftJson(config =>
{
    //All JSON returns lowerCamelCase (JSON Standard - By Google) instead of PascalCase (C# Standard - By Microsoft)
    //References: 
    //JSON Standards by Google https://google.github.io/styleguide/jsoncstyleguide.xml?showone=Property_Name_Format#Property_Name_Format
    //C# Standards by Microsoft https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/capitalization-conventions
    config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

    //Trick for handling/ignoring Reference Loop Handling
    config.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("DevelopmentCorsPolicy");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseAuthentication();
app.Run();
