using Ewenze.Application.Common.Exceptions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Application.Services.Listings.Exceptions
{
    public class ListingException : ApplicationException<ListingExceptionReason>
    {
        public ListingException() { }
        public ListingException(string message) : base(message) { }
        public ListingException(string message, Exception inner) : base(message, inner) { }

        public ListingException(string message, ValidationResult validationErrors) : base(message)
        {
            Reason = ListingExceptionReason.InvalidProperty;
            ValidationErrors = validationErrors.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );
        }

        public IDictionary<string, string[]> ValidationErrors { get; set; }
        
    }
    public enum ListingExceptionReason
    {
        None,
        EntityNotFound,
        InvalidProperty
    }
}
