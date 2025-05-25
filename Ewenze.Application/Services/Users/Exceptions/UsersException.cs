using Ewenze.Application.Common.Exceptions;
using FluentValidation.Results;

namespace Ewenze.Application.Services.Users.Exceptions
{
    public class UsersException : ApplicationException<UsersExceptionReason>
    {
        public UsersException() { }

        public UsersException(string message) : base(message) { }

        public UsersException(string message, Exception inner) : base(message, inner) { }

        public UsersException(string message, ValidationResult validationResult) : base(message)
        {
            Reason = UsersExceptionReason.InvalidProperty;

            ValidationErrors = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );
        }

        public IDictionary<string, string[]> ValidationErrors { get; set; }
    }

    public enum UsersExceptionReason
    {
        None, 
        EntityNotFound, 
        InvalidProperty
    }
}
