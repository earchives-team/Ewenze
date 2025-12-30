
using Ewenze.Application.Common.Exceptions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace Ewenze.Application.Exceptions
{
    public class BadRequestException : ApplicationExceptionV2
    {
        public override int StatusCode => StatusCodes.Status400BadRequest;

        public override IDictionary<string, string[]>? Errors { get; }

        public BadRequestException(string message)
            : base(message)
        {
        }

        public BadRequestException(string message, ValidationResult validationResult)
        : base(message)
        {
            ValidationErrors = validationResult.ToDictionary();
        }

        public BadRequestException(string message, string field, string error) : base(message)
        {
            Errors = new Dictionary<string, string[]>
            {
                { field, new[] { error } }
            };
        }

        public IDictionary<string, string[]> ValidationErrors { get; set; }
    }

    public sealed class NotFoundException : ApplicationExceptionV2
    {
        public override int StatusCode => StatusCodes.Status404NotFound;

        public NotFoundException(string name, object key)
        : base($"The {name} {key} was not found.")
        {
        }
    }

    public sealed class ConflictException : ApplicationExceptionV2
    {
        public override int StatusCode => StatusCodes.Status409Conflict;
        public ConflictException(string message)
            : base(message)
        {
        }
    }
}
