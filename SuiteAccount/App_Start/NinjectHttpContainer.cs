using System.Reflection;
using System.Web.Http;
using Ninject;
using Ninject.Modules;
using SuiteAccount.Infrastructures.Modules;

namespace SuiteAccount
{
    public class NinjectHttpContainer
    {
        private static NinjectBootstrapper.NinjectHttpResolver _resolver;

        // Register Ninject Modules
        public static void RegisterModules()
        {
            _resolver = new NinjectBootstrapper.NinjectHttpResolver(GetModules());
            GlobalConfiguration.Configuration.DependencyResolver = _resolver;
        }

        public static void RegisterAssembly()
        {
            _resolver = new NinjectBootstrapper.NinjectHttpResolver(Assembly.GetExecutingAssembly());
            
            // This is where the actual hookup to the Web API Pipeline is done.
            GlobalConfiguration.Configuration.DependencyResolver = _resolver;
        }

        // Manually Resolve Dependencies
        public static T Resolve<T>()
        {
            return _resolver.Kernel.Get<T>();
        }

        /// <summary>
        /// Load Ninject modules
        /// </summary>
        /// <returns></returns>
        private static INinjectModule[] GetModules()
        {
            return new INinjectModule[]
            {
                new LoggingModule(),
                new ServiceBusModule(),
                new EventStoreRepositoryModule(),
                new ProvidersModule(),
                new QuerySqlModule(),
                new QueryNoSqlModule()
            };
        }
    }
}