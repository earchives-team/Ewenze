using Ewenze.Domain.Exceptions;

namespace Ewenze.API.Helpers
{
    public static class ExceptionStatusCodeMapper
    {
        private static readonly IDictionary<Type, int> ExceptionStatusCodeMapping = new Dictionary<Type, int>
        {
            { typeof(NotFoundException), StatusCodes.Status404NotFound },

        };


        public static int GetStatusCodeForException(Exception exception)
        {
            return ExceptionStatusCodeMapping.TryGetValue(exception.GetType(), out var code)
                ? code
                : StatusCodes.Status500InternalServerError;
        }
    }
}
