using System;
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
                new IPEndPoint(IPAddress.Parse(SuiteAccountConfiguration.EventStoreSection.Uri),
                    SuiteAccountConfiguration.EventStoreSection.Port);

            var connectionSettings = ConnectionSettings.Create();
            connectionSettings.SetDefaultUserCredentials(
                new UserCredentials(SuiteAccountConfiguration.EventStoreSection.User,
                    SuiteAccountConfiguration.EventStoreSection.Password));

            connectionSettings.SetHeartbeatTimeout(new TimeSpan(0, 5, 0));
            connectionSettings.SetHeartbeatInterval(new TimeSpan(0, 0, 25));

            return EventStoreConnection.Create(connectionSettings, tcpEndPoint);
        }
    }
}
