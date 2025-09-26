using Ewenze.Application.Common.Exceptions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Application.Services.ListingTypes.Exceptions
{
    public class ListingTypeException : ApplicationException<ListingTypeExceptionReason>
    {
        public ListingTypeException() { }
        public ListingTypeException(string message) : base(message) { }
        public ListingTypeException(string message, Exception inner) : base(message, inner) { }

        public ListingTypeException(string message, ValidationResult validationErrors) : base(message)
        {
            Reason = ListingTypeExceptionReason.InvalidProperty;
            ValidationErrors = validationErrors.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );
        }

        public IDictionary<string, string[]> ValidationErrors { get; set; }
    }

    public enum ListingTypeExceptionReason
    {
        None,
        EntityNotFound,
        InvalidProperty
    }
}
