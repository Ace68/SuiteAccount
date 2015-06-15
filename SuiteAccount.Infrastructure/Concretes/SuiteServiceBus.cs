using System;
using System.Collections.Generic;

using SuiteAccount.Infrastructure.Abstracts;
using SuiteAccount.Logging.Abstracts;

namespace SuiteAccount.Infrastructure.Concretes
{
    public class SuiteServiceBus : IServiceBus, IDisposable, IEventBus
    {
        private readonly Dictionary<Type, List<Action<MessageBase>>> _routes = new Dictionary<Type, List<Action<MessageBase>>>();
        private readonly ILogService _logService;

        public SuiteServiceBus(ILogService logService)
        {
            this._logService = logService;
        }

        public void Send<T>(T command) where T : CommandBase
        {
            if (command == null)
                throw new ArgumentNullException("command");

            List<Action<MessageBase>> handlers;
            if (this._routes.TryGetValue(typeof (T), out handlers))
            {
                this._logService.LoggerTrace(string.Format("Invio Comando {0}; AggregateId {1}", command.GetType(), command.AggregateId));
                handlers[0](command);
            }
            else
            {
                this._logService.LoggerTrace(string.Format("Nessun CommandHandler Trovato per il Comando {0}", command.GetType()));
                throw new Exception(string.Format("Nessun CommandHandler Trovato per il Comando {0}", command.GetType()));
            }

            //this._container.Resolve<IHandleCommand<T>>().Handle(command);
        }

        public void RegisterHandler<T>(Action<T> handler) where T : MessageBase
        {
            List<Action<MessageBase>> handlers;
            if (!_routes.TryGetValue(typeof (T), out handlers))
            {
                handlers = new List<Action<MessageBase>>();
                this._routes.Add(typeof(T), handlers);
            }
            handlers.Add(DelegateAdjuster.CastArgument<MessageBase, T>(x => handler(x)));
        }

        public void Publish(EventBase @event)
        {
            List<Action<MessageBase>> handlers;
            if (!this._routes.TryGetValue(@event.GetType(), out handlers)) return;

            foreach (var handler in handlers)
            {
                this._logService.LoggerTrace(string.Format("Evento {0}; AggregateId {1}", @event.GetType(), @event.AggregateId));
                handler(@event);
            }
        }

        #region Dispose
        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    // Qui si devono liberare le risorse eventualmente allocate da questo oggetto
                    // In questo caso ... nothing to do!!!
                }
            }
            this._disposed = true;
        }
        #endregion
    }
}
