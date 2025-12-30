using Ewenze.API.Helpers;
using Ewenze.Application.Common.Exceptions;
namespace Ewenze.API.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                // Essayer de passer au HttpContext suivant si on a un Exception on va aller dans le catch 
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // Methode permettant de gerer l'excption 
                await HandleExceptionAsync(httpContext, ex);

            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            int statusCode;
            IDictionary<string, string[]>? validationErrors = null;

            if (exception is ApplicationExceptionV2 appEx)
            {
                statusCode = appEx.StatusCode;
                validationErrors = appEx.Errors;
            }
            else
            {
                statusCode = StatusCodes.Status500InternalServerError;
            }

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;
            
            var problemDetails = new CustomProblemDetails
            {
                Title = exception.Message,
                Status = statusCode,
                Detail = exception.InnerException?.Message,
                Type = exception.GetType().Name,
                ErrorDetails = validationErrors
            };

            return httpContext.Response.WriteAsJsonAsync(problemDetails);
        }

    }
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
