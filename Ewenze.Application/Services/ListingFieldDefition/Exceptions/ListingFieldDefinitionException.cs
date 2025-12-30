using Ewenze.Application.Common.Exceptions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Application.Services.ListingFieldDefition.Exceptions
{
    // Ne peut plus etre utilisee car ApplicationException n'existe plus

    //public class ListingFieldDefinitionException : ApplicationException<ListingFieldDefinitionExceptionReason>
    //{
    //    public ListingFieldDefinitionException() { }
    //    public ListingFieldDefinitionException(string message) : base(message) { }
    //    public ListingFieldDefinitionException(string message, Exception inner) : base(message, inner) { }

    //    public ListingFieldDefinitionException(string message, ValidationResult validationErrors) : base(message)
    //    {
    //        Reason = ListingFieldDefinitionExceptionReason.InvalidProperty;
    //        ValidationErrors = validationErrors.Errors
    //            .GroupBy(e => e.PropertyName)
    //            .ToDictionary(
    //                g => g.Key,
    //                g => g.Select(e => e.ErrorMessage).ToArray()
    //            );
    //    }

    //    public IDictionary<string, string[]> ValidationErrors { get; set; }

    //}
    //public enum ListingFieldDefinitionExceptionReason
    //{
    //    None,
    //    EntityNotFound,
    //    InvalidProperty
    //}
}
