using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace Netmedia.Infrastructure.EntityFramework.Conventions
{
    // NOT WORKING!!
    public class RenameManyToManyKeys : IStoreModelConvention<EntitySet>
    {
        public void Apply(EntitySet set, DbModel model)
        {
            var properties = set.ElementType.Properties;
            if (properties.Count == 2)
            {
                var relationEnds = new List<string>();
                int i = 0;
                foreach (var metadataProperty in properties)
                {
                    if (metadataProperty.Name.EndsWith("_ID"))
                    {
                        var name = metadataProperty.Name;
                        relationEnds.Add(name.Substring(0, name.Length - 3));
                        i++;
                    }
                }
                if (relationEnds.Count == 2)
                {
                    set.Table = relationEnds.ElementAt(0) + "_" + relationEnds.ElementAt(1) + "_RelationTable";
                }
            }
        }
    }
}