using System;

using SuiteAccount.Domain.Events;
using SuiteAccount.Infrastructure.Abstracts;
using SuiteAccount.SqlModel.Denormalizer.Abstracts;

namespace SuiteAccount.SqlModel.Denormalizer.DomainEventsHandlers
{
    public class AccountEventsHandler:
        IEventHandler<AccountCreated>,
        IEventHandler<EmailUpdated>
    {
        private readonly IAccountDenormalizer _accountDenormalizer;

        public AccountEventsHandler(IAccountDenormalizer accountDenormalizer)
        {
            this._accountDenormalizer = accountDenormalizer;
        }

        public void Handle(AccountCreated @event)
        {
            if (@event.AggregateId == Guid.Empty) return;

            this._accountDenormalizer.CreateAccountAsync(@event.AggregateId, @event.UserName, @event.Password).Wait();
        }

        public void Handle(EmailUpdated @event)
        {
            if (@event.AggregateId == Guid.Empty) return;

            this._accountDenormalizer.UpdateEmailAsync(@event.AggregateId, @event.Email).Wait();
        }
    }
}
