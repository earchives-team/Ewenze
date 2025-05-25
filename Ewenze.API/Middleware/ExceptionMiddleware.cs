using Ewenze.API.Helpers;
using Ewenze.Application.Exceptions;
using Ewenze.Application.Services.Users.Exceptions;
using Ewenze.Domain.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;

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
            var statusCode = ExceptionStatusCodeMapper.GetStatusCodeForException(exception);

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;

            var problemDetails = new CustomProblemDetails
            {
                Title = exception.Message,
                Status = (int)statusCode,
                Detail = exception.InnerException?.Message,
                Type = exception.GetType().Name,
            };


            // Add specific errors if it's BadRequestException Can Help if someone does not put value we expected 
            if (exception is BadRequestException badRequestEx)
            {
                problemDetails.ErrorDetails = badRequestEx.ValidationErrors;
            }
            if (exception is UsersException userEx && userEx.ValidationErrors != null)
            {
                problemDetails.ErrorDetails = userEx.ValidationErrors;
            }

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
