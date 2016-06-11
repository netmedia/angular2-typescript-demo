namespace Netmedia.Domain.Validations.Normalization
{
    public class NormalizationValidationsBase : ValidationsBase, INormalizationValidations
    {
        public virtual bool IsLastName(string value)
        {
            return true;
        }
        public virtual bool IsPostCode(string value)
        {
            return true;
        }
        public virtual bool AreInitials(string value)
        {
            return true;
        }
        public virtual bool IsPhoneNumber(string value)
        {
            return true;
        }
        public virtual bool IsMobileNumber(string value)
        {
            return true;
        }
        public virtual bool IsFaxNumber(string value)
        {
            return true;
        }
        public virtual bool IsBankAccountNumber(string value)
        {
            return true;
        }
        public virtual bool IsSocialNumber(string value)
        {
            return true;
        }
        public virtual bool IsHouseNumber(string value)
        {
            return true;
        }
    }
}