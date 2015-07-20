using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Dependencies;

using Ninject;
using Ninject.Modules;
using SuiteAccount.EventStore.Persistence;
using SuiteAccount.Infrastructures.Modules;

namespace SuiteLogin
{
    public class NinjectBootstrapper
    {
        public class NinjectHttpResolver : IDependencyResolver
        {
            public IKernel Kernel { get; private set; }
            public NinjectHttpResolver(params INinjectModule[] modules)
            {
                Kernel = new StandardKernel(modules);

                RegisterHandlerInBus.Register(Kernel, true);

                this.StartEventsDispatching();
            }

            private void StartEventsDispatching()
            {
                var dispatcher = Kernel.Get<EventDispatcher>();
                dispatcher.StartDispatching();
            }

            public NinjectHttpResolver(Assembly assembly)
            {
                Kernel = new StandardKernel();
                Kernel.Load(assembly);
            }

            public object GetService(Type serviceType)
            {
                return Kernel.TryGet(serviceType);
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                return Kernel.GetAll(serviceType);
            }

            public void Dispose()
            {
                //Do Nothing
            }

            public IDependencyScope BeginScope()
            {
                return this;
            }
        }
    }
}