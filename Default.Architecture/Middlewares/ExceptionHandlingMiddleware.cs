using Business.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Default.Architecture.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger logger;
        public ExceptionHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            this.logger = loggerFactory.CreateLogger<ExceptionHandlingMiddleware>();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (BusinessException ex) //Business Exceptions
            {
                logger.LogInformation(ex.Message);
                httpContext.Response.StatusCode = 500;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    message = ex.Message,
                }));
            }
            catch (ValidationException ex) //Validation Exceptions
            {
                logger.LogInformation(ex.Message);
                httpContext.Response.StatusCode = 400;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    message = ex.Errors.Select(error => new
                    {
                        property = error.PropertyName,
                        error = error.ErrorMessage
                    })
                }
                ));
            }
            catch (Exception ex) //Unhandled Exceptions
            {
                var guid = Guid.NewGuid().ToString();

                var errorLog = new StringBuilder();
                errorLog.AppendLine($"An error occurred (Guid = {guid}): {ex.Message}");
                errorLog.AppendLine($"Request Path: {httpContext.Request.Path}");
                errorLog.AppendLine($"Request Method: {httpContext.Request.Method}");
                errorLog.AppendLine($"Request Headers:");
                foreach (var header in httpContext.Request.Headers)
                {
                    errorLog.AppendLine($"\t{header.Key}:{header.Value}");
                }
                //string body = new StreamReader(httpContext.Request.Body).ReadToEnd();
                //errorLog.AppendLine($"Request Body: {body}");

                logger.LogError(errorLog.ToString());

                httpContext.Response.StatusCode = 500;

                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    message = $"An error ocurred with Guid = {guid}. Contact the System Administration to fix this problem"
                }));

            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
