using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Netmedia.Domain.Validations
{
    public abstract class ValidationsBase
    {
        private readonly string _regExNumeric = @"^\d+$";
        private readonly string _regExNonNegativeNumeric = @"^[^-](\d)*$";
        private readonly string _regExNonNegativeInteger = @"^\d\d*$";
        private readonly string _regExLettersOnly = @"^(\p{L}{1,})$"; // "^[a-zA-Z]{1,}$";
        private readonly string _regExLettersAndDotOnly = @"^((\p{L}|\.){1,})$";
        private readonly string _regExCapitalLettersAndDotOnly = @"^((\p{Lu}|\.){1,})$";
        private readonly string _regExLettersWithApostrophAndSpace = @"^((\p{L}|'| ){1,})$";
        private readonly string _regExEmail = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        /// <summary>
        /// Validates if parameter exists.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        protected void ValidateParameterExists(string parameterName, string parameterValue)
        {
            if (IsEmptyOrUndefined(parameterValue)) throw new ArgumentNullException(parameterName);
        }
        /// <summary>
        /// Validates if parameter exists.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        protected void ValidateParameterExists(string parameterName, object parameterValue)
        {
            if (IsEmptyOrUndefined(parameterValue)) throw new ArgumentNullException(parameterName);
        }
        /// <summary>
        /// Validates if parameter exists.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        protected void ValidateParameterExists(string parameterName, DateTime? parameterValue)
        {
            if (parameterValue.HasValue) throw new ArgumentNullException(parameterName);
        }
        /// <summary>
        /// Checks is string empty or undefined.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool IsEmptyOrUndefined(string value)
        {
            return (string.IsNullOrEmpty(value));
        }
        /// <summary>
        /// Checks is object empty or undefined.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool IsEmptyOrUndefined(object value)
        {
            return (value == null);
        }
        /// <summary>
        /// Check if string matches expression.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="matchExpression"></param>
        /// <returns></returns>
        protected bool IsMatch(string value, string matchExpression)
        {
            return (new Regex(matchExpression)).IsMatch(value);
        }
        /// <summary>
        /// Gets date from string.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        protected DateTime GetDate(string value, DateTimeFormatInfo format)
        {
            DateTime convertedDate;
            DateTime.TryParse(value, format, DateTimeStyles.None, out convertedDate);
            return convertedDate;
        }
        /// <summary>
        /// Check is string a valid date.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        protected bool IsValidDate(string value, DateTimeFormatInfo format)
        {
            DateTime dummyDate;
            return DateTime.TryParse(value, format, DateTimeStyles.None, out dummyDate);
        }
        /// <summary>
        /// Gets age.
        /// </summary>
        /// <param name="birthDate"></param>
        /// <returns></returns>
        protected int GetAge(DateTime birthDate)
        {
            int age = DateTime.Now.Year - birthDate.Year;

            if ((DateTime.Now.Month < birthDate.Month) ||
                (DateTime.Now.Month == birthDate.Month && DateTime.Now.Day < birthDate.Day))
            {
                age -= 1;
            }

            return age;
        }
        /// <summary>
        /// Checks are two values filled or empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="value"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        protected bool Are2ValuesFilledOrEmpty<T, T2>(T? value, T2? value2)
            where T : struct
            where T2 : struct
        {
            return ((!value.HasValue && !value2.HasValue) || (value.HasValue && value2.HasValue));
        }
        /// <summary>
        /// Checks are 3 values filled or empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="value"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        /// <returns></returns>
        protected bool Are3ValuesFilledOrEmpty<T, T2, T3>(T? value, T2? value2, T3? value3)
            where T : struct
            where T2 : struct
            where T3 : struct
        {
            return
                ((!value.HasValue && !value2.HasValue && !value3.HasValue) ||
                 (value.HasValue && value2.HasValue && value3.HasValue));
        }
        /// <summary>
        /// Checks are 4 values filled or empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="value"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        /// <param name="value4"></param>
        /// <returns></returns>
        protected bool Are4ValuesFilledOrEmpty<T, T2, T3, T4>(T? value, T2? value2, T3? value3, T4? value4)
            where T : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
        {
            return
                ((!value.HasValue && !value2.HasValue && !value3.HasValue && !value4.HasValue) ||
                 (value.HasValue && value2.HasValue && value3.HasValue && value4.HasValue));
        }



        /// <summary>
        /// Checks is string empty.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool IsEmpty(string value)
        {
            return IsEmptyOrUndefined(value);
        }
        /// <summary>
        /// Check if string is not empty.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool IsNotEmpty(string value)
        {
            return (!IsEmptyOrUndefined(value));
        }
        /// <summary>
        /// Checks is string numeric.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool IsNumeric(string value)
        {
            ValidateParameterExists("Value", value);
            return IsMatch(value, _regExNumeric);
        }
        /// <summary>
        /// Checks is number non negative.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool IsNonNegativeNumeric(int value)
        {
            return (value >= 0);
        }
        /// <summary>
        /// Checks is string non negative numeric.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool IsNonNegativeNumeric(string value)
        {
            ValidateParameterExists("Value", value);
            return IsMatch(value, _regExNonNegativeNumeric);
        }

        protected bool IsNonNegativeInteger(int value)
        {
            return (value >= 0);
        }
        protected bool IsNonNegativeInteger(string value)
        {
            ValidateParameterExists("Value", value);
            return IsMatch(value, _regExNonNegativeInteger);
        }

        /// <summary>
        /// Check is string made only of letters.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool IsNotLoading(string value)
        {
            ValidateParameterExists("Value", value);
            return !IsMatch(value, "Laden...") & !IsMatch(value, "Loading...");
        }

        /// <summary>
        /// Check is string made only of letters.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool IsLettersOnly(string value)
        {
            ValidateParameterExists("Value", value);
            return IsMatch(value, _regExLettersOnly);
        }
        /// <summary>
        /// Check is string made only of letters and dot.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool IsLettersAndDotOnly(string value)
        {
            ValidateParameterExists("Value", value);
            return IsMatch(value, _regExLettersAndDotOnly);
        }
        /// <summary>
        /// Check is string made only of capital letters and dot.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool IsCapitalLettersAndDotOnly(string value)
        {
            ValidateParameterExists("Value", value);
            return IsMatch(value, _regExCapitalLettersAndDotOnly);
        }


        /// <summary>
        /// Check is string made only of letters with possible apostrophe and space.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool IsLettersWithApostrophAndSpace(string value)
        {
            ValidateParameterExists("Value", value);
            return IsMatch(value, _regExLettersWithApostrophAndSpace);
        }
        
        /// <summary>
        /// Check is string a date.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool IsDate(string value)
        {
            return IsValidDate(value, CultureInfo.CurrentCulture.DateTimeFormat);
        }
        /// <summary>
        /// Check is string a date and of proper date time format.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        protected bool IsDate(string value, DateTimeFormatInfo format)
        {
            ValidateParameterExists("Value", value);
            ValidateParameterExists("Format", format);
            return IsValidDate(value, format);
        }
        /// <summary>
        /// Check if string is of valid length
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        protected bool IsLengthValid(string value, int length)
        {
            ValidateParameterExists("Value", value);
            ValidateParameterExists("Length", length);
            return value.Length == length;
        }
        /// <summary>
        /// Check if string is of valid length or less
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        protected bool IsLengthLessThenOrEqualyValid(string value, int length)
        {
            ValidateParameterExists("Value", value);
            ValidateParameterExists("Length", length);
            return value.Length <= length;
        }
        /// <summary>
        /// Check if string is a email address
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool IsEmail(string value)
        {
            ValidateParameterExists("Value", value);
            return IsMatch(value, _regExEmail);
        }

        protected bool IsMaximalNumberOfCheckBoxItemsCheckedValid(string value, int length)
        {
            ValidateParameterExists("Value", value);
            ValidateParameterExists("Length", length);
            
            return value.Split('t').Length - 1 <= length;
        }

    }
}