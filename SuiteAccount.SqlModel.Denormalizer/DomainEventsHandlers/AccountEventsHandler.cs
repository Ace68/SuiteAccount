using System;

using SuiteAccount.Messages.Events;
using SuiteAccount.Infrastructure.Abstracts;
using SuiteAccount.SqlModel.Services.Abstracts;

namespace SuiteAccount.SqlModel.Denormalizer.DomainEventsHandlers
{
    public class AccountEventsHandler:
        IEventHandler<AccountCreated>,
        IEventHandler<EmailUpdated>
    {
        private readonly ISqlAccountService _sqlAccountService;

        public AccountEventsHandler(ISqlAccountService sqlAccountService)
        {
            this._sqlAccountService = sqlAccountService;
        }

        public void Handle(AccountCreated @event)
        {
            if (@event.AggregateId == Guid.Empty) return;

            this._sqlAccountService.CreateAccountAsync(@event.AggregateId, @event.UserName, @event.Password).Wait();
        }

        public void Handle(EmailUpdated @event)
        {
            if (@event.AggregateId == Guid.Empty) return;

            this._sqlAccountService.UpdateEmailAsync(@event.AggregateId, @event.Email).Wait();
        }
    }
}
