using System;

using SuiteAccount.Messages.Events;
using SuiteAccount.Infrastructure.Abstracts;
using SuiteAccount.NoSql.Services.Abstracts;

namespace SuiteAccount.NoSql.Denormalizer.DomainEventsHandlers
{
    public class NoSqlAccountEventsHandler:
        IEventHandler<AccountCreated>,
        IEventHandler<EmailUpdated>
    {
        private readonly INoSqlAccountService _noSqlAccountService;

        public NoSqlAccountEventsHandler(INoSqlAccountService noSqlAccountService)
        {
            this._noSqlAccountService = noSqlAccountService;
        }

        public void Handle(AccountCreated @event)
        {
            if (@event.AggregateId == Guid.Empty) return;

            this._noSqlAccountService.CreateAccountAsync(@event.AggregateId, @event.UserName, @event.Password);
        }

        public void Handle(EmailUpdated @event)
        {
            if (@event.AggregateId == Guid.Empty) return;

            this._noSqlAccountService.UpdateEmailAsync(@event.AggregateId, @event.Email);
        }
    }
}
