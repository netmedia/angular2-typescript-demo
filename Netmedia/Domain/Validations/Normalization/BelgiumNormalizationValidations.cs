namespace Netmedia.Domain.Validations.Normalization
{
    public class BelgiumNormalizationValidations : NormalizationValidationsBase
    {
        public override bool IsPostCode(string value)
        {
            ValidateParameterExists("Value", value);
            return IsNumeric(value) && IsLengthValid(value, 10);
        }
    }
}