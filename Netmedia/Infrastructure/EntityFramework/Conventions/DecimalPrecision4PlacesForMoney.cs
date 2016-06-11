using System.Data.Entity.ModelConfiguration.Conventions;

namespace Netmedia.Infrastructure.EntityFramework.Conventions
{
    public class DecimalPrecision4PlacesForMoney : Convention
    {
        public DecimalPrecision4PlacesForMoney()
        {
            Properties<decimal>().Configure(config => config.HasPrecision(10, 4));
        }
    }
}