using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(ex.Errors.Select(error => new { error = error.ErrorMessage, property = error.PropertyName })));
            }
            catch (Exception ex) //Unhandled Exceptions
            {
                var guid = Guid.NewGuid().ToString();

                var errorLog = new StringBuilder();
                errorLog.Append($"An error occurred (Guid = {guid}): {ex.Message}");
                errorLog.Append($"Stacktrace: {ex.StackTrace}");
                errorLog.Append($"Request Path: {httpContext.Request.Path}");
                errorLog.Append($"Request Method: {httpContext.Request.Method}");
                errorLog.Append($"Request Header: {httpContext.Request.Headers.Select(x => $"{x.Key}:{x.Value}")}");
                errorLog.Append($"Request Body: {httpContext.Request.Body}");

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
