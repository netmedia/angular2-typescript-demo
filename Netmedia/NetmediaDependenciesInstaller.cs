using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Netmedia.Infrastructure.Services;
using Netmedia.Web.Services;

namespace Netmedia
{
    public class NetmediaDependenciesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ICacheService>().ImplementedBy<CacheService>());
            container.Register(Component.For<IFileSystemService>().ImplementedBy<FileSystemService>());
            container.Register(Component.For<IEmailService>().ImplementedBy<SmtpClientEmailService>());
            container.Register(Component.For<IExcelService>().ImplementedBy<ExcelService>());
        }
    }
}
