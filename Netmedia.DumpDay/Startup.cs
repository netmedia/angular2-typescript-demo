using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Netmedia.DumpDay.Startup))]
namespace Netmedia.DumpDay
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
