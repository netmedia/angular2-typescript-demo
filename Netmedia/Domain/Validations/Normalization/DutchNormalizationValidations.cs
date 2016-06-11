namespace Netmedia.Domain.Validations.Normalization
{
    public class DutchNormalizationValidations : NormalizationValidationsBase
    {
        private readonly string _regExLastName = @"^\d{4}\s[a-zA-Z]{2}$";
        private readonly string _regExPostCode = @"^\d{4}\s[a-zA-Z]{2}$";
        private readonly string _regExPhoneNumber = @"0[0-9]{9}"; // @"^\(?(0)[1-9]{2}\)?[0-9]{7}$|^\(?(0)[1-9]{3}\)?[0-9]{6}$"; // @"^(((?<!(06))\d){10}|((?<!(06))\d){3}-(\d){7}|((?<!(06))\d){4}-(\d){6})$";              
        private readonly string _regExMobilePhoneNumber = @"^(06)((\d){8}|(\d){1}-(\d){7}|(\d){2}-(\d){6})$";

        /// <summary>
        /// Checks is valid postcode.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsPostCode(string value)
        {
            ValidateParameterExists("Value", value);
            return IsMatch(value, _regExPostCode);
        }
        /// <summary>
        /// Checks are initials valid.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool AreInitials(string value)
        {
            ValidateParameterExists("Value", value);
            return IsLettersAndDotOnly(value);
        }
        /// <summary>
        /// Checks is valid phone number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsPhoneNumber(string value)
        {
            ValidateParameterExists("Value", value);
            return IsMatch(value, _regExPhoneNumber);
        }
        /// <summary>
        /// Checks is mobile number valid.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsMobileNumber(string value)
        {
            ValidateParameterExists("Value", value);
            return IsMatch(value, _regExMobilePhoneNumber);
        }
        /// <summary>
        /// Check if it is a fax number
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsFaxNumber(string value)
        {
            return IsPhoneNumber(value);
        }
        /// <summary>
        /// Checks id bank account number valid.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsBankAccountNumber(string value)
        {
            ValidateParameterExists("Value", value);
            return _IsBankNumber(value);
        }
        /// <summary>
        /// Checks is social number valid.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsSocialNumber(string value)
        {
            ValidateParameterExists("Value", value);
            return _IsSocialNumber(value);
        }
        /// <summary>
        /// Check if housenumber is valid.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsHouseNumber(string value)
        {
            return IsNumeric(value);
        }


        /// <summary>
        /// Checks is valid bank number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool _IsBankNumber(string value)
        {
            if (value == "") return true;

            value = value.Trim();

            if (value.Length >= 4 && value.Length <= 8)
            {
                if (!IsNonNegativeInteger(value) && value.Length <= 8)
                {
                    if (!IsNonNegativeInteger(value.Substring(1, value.Length - 1))) return false;
                    if (value.IndexOf("P") != 0) return false;
                }

                if (IsNonNegativeInteger(value) && value.Length <= 7) return true;
            }

            if (value.Length == 9) value = '0' + value;

            if (value.Length == 10 && IsNonNegativeInteger(value))
            {
                int number = 0;
                int digit = 0;
                int multipleNumber = 10;

                for (int i = 0; i <= 9; i++)
                {
                    digit = int.Parse(value.Substring(i, 1));
                    number += (multipleNumber * digit);
                    multipleNumber -= 1;
                }

                if ((number % 11) == 0) return true;
            }

            return false;
        }
        /// <summary>
        /// Checks is valid social number.
        /// </summary>
        /// <param name="socialNumber"></param>
        /// <returns></returns>
        private bool _IsSocialNumber(string socialNumber)
        {
            int summedValue = 0;
            int lastDigit = 0;
            int socialNumberLen = 0;

            socialNumber = socialNumber.Trim();
            socialNumberLen = socialNumber.Length;

            if (!IsNumeric(socialNumber) || socialNumberLen != 9) return false;

            for (int i = 1; i <= socialNumberLen; i++)
            {
                summedValue += int.Parse(socialNumber.Substring(socialNumberLen - i, 1)) * i;
            }
            lastDigit = int.Parse(socialNumber.Substring(socialNumberLen - 1, 1));
            summedValue -= lastDigit;

            return ((summedValue % 11) == lastDigit);
        }
    }
}