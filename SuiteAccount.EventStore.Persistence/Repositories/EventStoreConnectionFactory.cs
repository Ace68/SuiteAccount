using System.Net;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using SuiteAccount.Configuration;

namespace SuiteAccount.EventStore.Persistence.Repositories
{
    public class EventStoreConnectionFactory
    {
        private static IEventStoreConnection _eventStoreConnection;

        public static IEventStoreConnection EventStore
        {
            get 
            { 
                return _eventStoreConnection ??
                    (_eventStoreConnection = CreateEventStoreConnection()); 
            }
        }

        private static IEventStoreConnection CreateEventStoreConnection()
        {
            var tcpEndPoint =
                new IPEndPoint(IPAddress.Parse(SuiteAccountConfiguration.SuiteAccountSection.EventStoreUri),
                    SuiteAccountConfiguration.SuiteAccountSection.EventStorePort);

            var connectionSettings = ConnectionSettings.Create();
            connectionSettings.SetDefaultUserCredentials(
                new UserCredentials(SuiteAccountConfiguration.SuiteAccountSection.EventStoreUser,
                    SuiteAccountConfiguration.SuiteAccountSection.EventStorePassword));
            
            return EventStoreConnection.Create(connectionSettings, tcpEndPoint);
        }
    }
}
