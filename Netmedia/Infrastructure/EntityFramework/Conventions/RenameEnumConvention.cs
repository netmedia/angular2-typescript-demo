using System;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Netmedia.Infrastructure.EntityFramework.Conventions
{
    public class RenameEnumConvention : Convention
    {
        public RenameEnumConvention()
        {
            Properties().Where(p => p.PropertyType.IsEnum 
                                || (Nullable.GetUnderlyingType(p.PropertyType) != null 
                                            && Nullable.GetUnderlyingType(p.PropertyType).IsEnum == true))
                    .Configure(p => p.HasColumnName(p.ClrPropertyInfo.Name + "Id"));
        }
    }
}