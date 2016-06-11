using Netmedia.Domain.Validations.Basic;
using Netmedia.Domain.Validations.Normalization;

namespace Netmedia.Domain.Validations
{
    public interface IDomainValidations
    {
        IBasicValidations Basic { get; set; }
        INormalizationValidations Normalization { get; set; }
    }
}