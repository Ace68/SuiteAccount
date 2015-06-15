using System;
using System.Collections.Generic;

using SuiteAccount.Domain.Infrastructure.Abstracts;
using SuiteAccount.Domain.Infrastructure.Concretes;

namespace SuiteAccount.Domain.DomainEvents
{
    public static class DomainEvent
    {
        private static List<Delegate> _actions;
        public static IDomainEventDispatcher Dispatcher { get; set; }

        public static void Register<T>(Action<T> callback) where T : EventBase
        {
            _actions = _actions ?? new List<Delegate>();
            _actions.Add(callback);
        }

        public static void ClearCallbacks()
        {
            _actions = null;
        }

        public static void RaiseEvent<T>(T @event) where T : EventBase
        {
            if (Dispatcher != null)
            {
                Dispatcher.Dispatch(@event);
            }

            if (_actions == null) return;

            foreach (var action in _actions)
            {
                if (action is Action<T>)
                {
                    ((Action<T>)action)(@event);
                }
            }
        }
    }
}
