using CommonDomain.Persistence;
using EventStore.ClientAPI;

using Ninject;
using Ninject.Modules;

using SuiteAccount.Infrastructure.Abstracts;
using SuiteAccount.EventStore.Persistence;
using SuiteAccount.EventStore.Persistence.Repositories;

namespace SuiteAccount.Infrastructures.Modules
{
    public class EventStoreRepositoryModule : NinjectModule
    {
        public override void Load()
        {
            var connection = EventStoreConnectionFactory.EventStore;
            connection.ConnectAsync().Wait();
            Bind<IEventStoreConnection>().ToConstant(connection).InSingletonScope();

            var eventStoreRepository = new EventStoreRepository(connection);
            Bind<IRepository>().ToConstant(eventStoreRepository).InSingletonScope();
            
            var dispatcher = new EventDispatcher(connection, this.Kernel.Get<IEventBus>());
            Bind<EventDispatcher>().ToConstant(dispatcher).InSingletonScope();
            
            // Non ha senso avviare ora il Dispatching degli eventi.
            // Prima termino tutte le registrazioni, poi, in fase di boot dell'applicazione
            // avvio il Dispatching e ricostruisco il ReadModel.
            //dispatcher.StartDispatching();
        }
    }
}
