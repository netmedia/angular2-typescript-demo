using System.Globalization;

namespace Netmedia.Domain.Validations.Basic
{
    public class BasicValidations : ValidationsBase, IBasicValidations
    {
        new public bool IsEmpty(string value)
        {
            return base.IsEmpty(value);
        }
        new public bool IsNotEmpty(string value)
        {
            return base.IsNotEmpty(value);
        }
        new public bool IsNumeric(string value)
        {
            return base.IsNumeric(value);
        }
        new public bool IsNonNegativeNumeric(int value)
        {
            return base.IsNonNegativeNumeric(value);
        }
        new public bool IsNonNegativeNumeric(string value)
        {
            return base.IsNonNegativeNumeric(value);
        }
        new public bool IsNotLoading(string value)
        {
            return base.IsNotLoading(value);
        }
        new public bool IsLettersOnly(string value)
        {
            return base.IsLettersOnly(value);
        }
        new public bool IsLettersAndDotOnly(string value)
        {
            return base.IsLettersAndDotOnly(value);
        }
        new public bool IsCapitalLettersAndDotOnly(string value)
        {
            return base.IsCapitalLettersAndDotOnly(value);
        }
        new public bool IsLettersWithApostrophAndSpace(string value)
        {
            return base.IsLettersWithApostrophAndSpace(value);
        }
        new public bool IsDate(string value)
        {
            return base.IsDate(value);
        }
        new public bool IsDate(string value, DateTimeFormatInfo format)
        {
            return base.IsDate(value, format);
        }
        new public bool IsLengthValid(string value, int length)
        {
            return base.IsLengthValid(value, length);
        }
        new public bool IsLengthLessThenOrEqualyValid(string value, int length)
        {
            return base.IsLengthLessThenOrEqualyValid(value, length);
        }
        new public bool IsEmail(string value)
        {
            return base.IsEmail(value);
        }
        new public bool IsEmptyOrUndefined(string value)
        {
            return base.IsEmptyOrUndefined(value);
        }
        new public bool IsMaximalNumberOfCheckBoxItemsCheckedValid(string value, int number)
        {
            return base.IsMaximalNumberOfCheckBoxItemsCheckedValid(value, number);
        }
    }
}