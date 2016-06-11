namespace Netmedia.Domain.Validations.Normalization
{
    public interface INormalizationValidations
    {
        /// <summary>
        /// Checks is last name.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool IsLastName(string value);

        /// <summary>
        /// Checks is valid postcode.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool IsPostCode(string value);

        /// <summary>
        /// Checks are initials valid.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool AreInitials(string value);

        /// <summary>
        /// Checks is valid phone number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool IsPhoneNumber(string value);

        /// <summary>
        /// Checks is mobile number valid.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool IsMobileNumber(string value);

        /// <summary>
        /// Check if it is a fax number
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool IsFaxNumber(string value);

        /// <summary>
        /// Checks id bank account number valid.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool IsBankAccountNumber(string value);

        /// <summary>
        /// Checks is social number valid.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool IsSocialNumber(string value);

        /// <summary>
        /// Check if housenumber is valid.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool IsHouseNumber(string value);
    }
}