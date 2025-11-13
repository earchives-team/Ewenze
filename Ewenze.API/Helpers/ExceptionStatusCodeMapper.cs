using Ewenze.Application.Common.Exceptions;
using Ewenze.Application.Services.Listings.Exceptions;
using Ewenze.Application.Services.ListingTypes.Exceptions;
using Ewenze.Application.Services.Users.Exceptions;
using Ewenze.Domain.Exceptions;

namespace Ewenze.API.Helpers
{
    public static class ExceptionStatusCodeMapper
    {
        private static readonly IDictionary<Enum, int> ReasonStatusCodeMapping = new Dictionary<Enum, int>
        {
            { UsersExceptionReason.EntityNotFound, StatusCodes.Status404NotFound },
            { ListingExceptionReason.EntityNotFound, StatusCodes.Status404NotFound },
            { ListingTypeExceptionReason.EntityNotFound, StatusCodes.Status404NotFound },
        };

        public static int GetStatusCodeForException(Exception exception)
        {

            if(exception is IAppExceptionWithReason withReason)
            {
                var reason = withReason.GetReason();

                if(ReasonStatusCodeMapping.TryGetValue(reason, out var code))
                    return  code;
            }

            return StatusCodes.Status500InternalServerError;
        }
    }
}
