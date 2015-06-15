using Ninject.Modules;
using SuiteAccount.Infrastructures.Modules;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SuiteAccount.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SuiteAccount.NinjectWebCommon), "Stop")]

namespace SuiteAccount
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var modules = GetModules();
            var kernel = new StandardKernel(modules);

            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
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
                new CommandHandlersModule(),
                new ServiceBusModule(),
                new EventHandlersModule(),
                new EventStoreRepositoryModule(),
                new ProvidersModule(),
                new QueryModelModule()
            };
        }

        /// <summary>
        /// Load Services and Module
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            RegisterHandlerInBus.Register(kernel, true);
        }        
    }
}
