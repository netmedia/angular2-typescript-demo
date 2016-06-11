using Castle.Windsor;

namespace Netmedia.Common
{
    /// <summary>
    /// Wanted to avoid having IoC class and directly use CommonServiceLocator 
    /// (although I don't like it) but as it's interface does not support sending prepared
    /// parameters to constructor I had to add this to directly send to Windsor parameters
    /// through constructor.
    /// I needed this because I'm sending a service to EF context (ISecurityService which 
    /// in turns gets the current user from db) which uses the EF context to retreive the user
    /// so I wanted to reuse the same context which calls that service.
    /// </summary>
    public class IoC
    {
        private static IWindsorContainer _container;
        private static readonly object _syncObject = new object();

        public static IWindsorContainer InitializeAndReturn()
        {
            if (_container == null)
            {
                lock (_syncObject)
                {
                    if (_container == null)
                    {
                        _container = new WindsorContainer();
                    }
                }
            }
            return _container;
        }
        public static IWindsorContainer Current
        {
            get
            {
                return _container;
            }
        }

        public static ResolvedType Resolve<ResolvedType>()
        {
            return Current.Resolve<ResolvedType>();
        }
        public static ResolvedType Resolve<ResolvedType>(object argumentsAsAnonymousType)
        {
            return Current.Resolve<ResolvedType>(argumentsAsAnonymousType);
        }
    }
}