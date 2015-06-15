using System.Web.Http;
using System.Web.Mvc;

using SuiteAccount.Controllers;
using SuiteAccount.Infrastructures.Modules;

namespace SuiteAccount
{
    public class Bootstrapper
    {


        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            #region Modules
            container.Resolve<SuiteAccountEventBoot>()
                .RegisterModule(typeof(ProvidersModule))
                .RegisterModule(typeof(ServiceBusModule))
                .RegisterModule(typeof(LoggingModule))
                .RegisterModule(typeof(CommandHandlersModule))
                .RegisterModule(typeof(EventHandlersModule))
                .RegisterModule(typeof(EventStoreRepositoryModule))
                .RegisterModule(typeof(QueryModelModule));
            #endregion

            #region Handlers
            RegisterHandlerInBus.Register(container, true);
            #endregion

            #region Controller
            container.RegisterType<HomeController>(new HierarchicalLifetimeManager());
            container.RegisterType<AccountController>(new HierarchicalLifetimeManager());
            #endregion

            RegisterTypes(container);
            return container;
        }

        /// <summary>
        /// Override the default dependency resolver with Unity
        /// </summary>
        /// <param name="container"></param>
        public static void RegisterTypes(IUnityContainer container)
        {
            GlobalConfiguration.Configuration.DependencyResolver = new IoCContainer(container);
        }
    }
}