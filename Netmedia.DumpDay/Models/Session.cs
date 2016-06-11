using System;
using System.Security.Policy;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Netmedia.DumpDay.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int Votes { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }

        [JsonIgnore, XmlIgnore]
        public string Domain
        {
            get
            {
                if (string.IsNullOrEmpty(Link) == false)
                {
                    Uri uri = null;
                    var isCreated = Uri.TryCreate(Link, UriKind.Absolute, out uri);
                    if (isCreated)
                    {
                        return uri.GetLeftPart(UriPartial.Authority);
                    }
                }                    

                return string.Empty;
            }
        }
    }
}