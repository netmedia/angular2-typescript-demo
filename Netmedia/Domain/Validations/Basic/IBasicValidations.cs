using System.Globalization;

namespace Netmedia.Domain.Validations.Basic
{
    public interface IBasicValidations
    {
        bool IsEmpty(string value);
        bool IsNotEmpty(string value);
        bool IsNumeric(string value);
        bool IsNonNegativeNumeric(int value);
        bool IsNonNegativeNumeric(string value);
        bool IsNotLoading(string value);
        bool IsLettersOnly(string value);
        bool IsLettersAndDotOnly(string value);
        bool IsLettersWithApostrophAndSpace(string value);
        bool IsCapitalLettersAndDotOnly(string value);
        bool IsDate(string value);
        bool IsDate(string value, DateTimeFormatInfo format);
        bool IsLengthValid(string value, int length);
        bool IsLengthLessThenOrEqualyValid(string value, int length);
        bool IsEmail(string value);
        bool IsEmptyOrUndefined(string value);
        bool IsMaximalNumberOfCheckBoxItemsCheckedValid(string value, int number);
    }
}