using Ewenze.Application.Services.Users.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ewenze.Application.Common.Helpers
{
    public class InvalidPropertyValidationHelper
    {
        private static readonly Regex emailRegex = new(
            @"(([^<>()[\]\.,;:\s@\""]+(\.[^<>()[\]\.,;:\s@\""]+)*)|(\"".+\""))@(([^<>()[\]\.,;:\s@\""]+\.)+[^<>()[\]\.,;:\s@\""]{2,})",
            RegexOptions.Compiled
        );

        public static bool ValidateRequired<T>(T propertyValue)
        {
            return !string.IsNullOrWhiteSpace($"{propertyValue}");
        }

        public static bool ValidateGuidRequired(Guid? propertyValue)
        {
            return propertyValue.HasValue && ValidateIsNotDefault(propertyValue.Value, Guid.Empty);
        }

        public static bool ValidateIsNotDefault<T>(T propertyValue, T defaultValue)
        {
            return !string.Equals($"{propertyValue}", $"{defaultValue}", StringComparison.Ordinal);
        }

        public static bool ValidateNullableEmail(string email)
        {
            return string.IsNullOrWhiteSpace(email) || ValidateRegexMatch(email, emailRegex);
        }

        public static bool ValidateInLongitudeRange(double longitude)
        {
            return ValidateRange(longitude, -180.0, 180.0);
        }

        public static bool ValidateInLatitudeRange(double latitude)
        {
            return ValidateRange(latitude, -90.0, 90.0);
        }

        public static bool ValidateMaxLength(string propertyValue, int maxLength)
        {
            return string.IsNullOrWhiteSpace(propertyValue) || propertyValue.Length <= maxLength;
        }

        public static bool ValidateRegexMatch(string propertyValue, Regex regex)
        {
            return regex.IsMatch(propertyValue);
        }

        public static bool ValidateRange<T>(T propertyValue, T minValue, T maxValue) where T : IComparable<T>
        {
            return propertyValue.CompareTo(minValue) >= 0 && propertyValue.CompareTo(maxValue) <= 0;
        }


        // Tout le code en commentaire en bas  sont Valid Pour le moment ils ne sont pas utiliser puisque l'application EWENZE
        // n'a pas encore etablie des regle d'entreprise
        // Il faudra changer UsersException par l'exception de l'entreprise 

        // DO NOT REMOVE THE CODE BELOW

        //public static void ValidateRequiredThanThrowIfFailed<T>(T propertyValue, string propertyName)
        //{
        //    if (!ValidateRequired(propertyValue))
        //        throw new UsersException($"{propertyName} is required") { Reason = UsersExceptionReason.InvalidProperty, InvalidProperty = propertyName };
        //}

        //public static void ValidateGuidRequiredThanThrowIfFailed(Guid? propertyValue, string propertyName)
        //{
        //    if (!ValidateGuidRequired(propertyValue))
        //        throw new UsersException($"{propertyName} is required") { Reason = UsersExceptionReason.InvalidProperty, InvalidProperty = propertyName };
        //}

        //public static void ValidateIsNotDefaultThanThrowIfFailed<T>(T propertyValue, string propertyName, T defaultValue)
        //{
        //    if (!ValidateIsNotDefault(propertyValue, defaultValue))
        //        throw new UsersException($"{propertyName} value cannot be left at: {defaultValue}") { Reason = UsersExceptionReason.InvalidProperty, InvalidProperty = propertyName };
        //}

        //public static void ValidateNullableEmailThanThrowIfFailed(string email, string propertyName)
        //{
        //    if (!ValidateNullableEmail(email))
        //        throw new UsersException($"{propertyName} must be a valid email address") { Reason = UsersExceptionReason.InvalidProperty, InvalidProperty = propertyName };
        //}

        //public static void ValidateInLongitudeRangeThanThrowIfFailed(double longitude, string propertyName)
        //{
        //    if (!ValidateInLongitudeRange(longitude))
        //        throw new UsersException($"{propertyName} must be between -180 and 180") { Reason = UsersExceptionReason.InvalidProperty, InvalidProperty = propertyName };
        //}

        //public static void ValidateInLatitudeRangeThanThrowIfFailed(double latitude, string propertyName)
        //{
        //    if (!ValidateInLatitudeRange(latitude))
        //        throw new UsersException($"{propertyName} must be between -90 and 90") { Reason = UsersExceptionReason.InvalidProperty, InvalidProperty = propertyName };
        //}

        //public static void ValidateMaxLengthThanThrowIfFailed(string propertyValue, string propertyName, int maxLength)
        //{
        //    if (!ValidateMaxLength(propertyValue, maxLength))
        //        throw new UsersException($"{propertyName} must not exceed {maxLength} characters")
        //        {
        //            Reason = UsersExceptionReason.InvalidProperty,
        //            InvalidProperty = propertyName
        //        };
        //}

        //public static void ValidateRegexMatchThanThrowIfFailed(string propertyValue, string propertyName, Regex regex)
        //{
        //    if (!ValidateRegexMatch(propertyValue, regex))
        //        throw new UsersException($"{propertyName} must match the required pattern {regex}")
        //        {
        //            Reason = UsersExceptionReason.InvalidProperty,
        //            InvalidProperty = propertyName
        //        };
        //}


        //public static void ValidateRangeThanThrowIfFailed<T>(T propertyValue, string propertyName, T minValue, T maxValue) where T : IComparable<T>
        //{
        //    if (!ValidateRange(propertyValue, minValue, maxValue))
        //        throw new UsersException($"{propertyName} must be between {minValue} and {maxValue}")
        //        {
        //            Reason = UsersExceptionReason.InvalidProperty,
        //            InvalidProperty = propertyName
        //        };
        //}
    }
}
