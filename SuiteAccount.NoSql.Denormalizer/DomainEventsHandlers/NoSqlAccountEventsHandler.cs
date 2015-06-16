using System;
using System.Linq;
using MongoDB.Driver;
using SuiteAccount.Domain.Events;
using SuiteAccount.Infrastructure.Abstracts;
using SuiteAccount.NoSql.Persistence.Abstracts;
using SuiteAccount.NoSql.Persistence.Entities;

namespace SuiteAccount.NoSql.Denormalizer.DomainEventsHandlers
{
    public class NoSqlAccountEventsHandler:
        IEventHandler<AccountCreated>,
        IEventHandler<EmailUpdated>
    {
        private readonly IDocumentRepository<NoSqlAccount> _accountRepository;

        //TODO: Implement a service to free Handlers by BusinessLogic.
        public NoSqlAccountEventsHandler(IDocumentRepository<NoSqlAccount> accountRepository)
        {
            this._accountRepository = accountRepository;
        }

        public void Handle(AccountCreated @event)
        {
            if (@event.AggregateId == Guid.Empty) return;

            var account = new NoSqlAccount
            {
                Id = @event.AggregateId.ToString("N"),
                UserName = @event.UserName,
                Password = @event.Password,
                IsApproved = false,
                IsLockedOut = true,
                IsOnLine = false,
                CreationDate = DateTime.UtcNow
            };

            this._accountRepository.InsertOneAsync(account);
        }

        public void Handle(EmailUpdated @event)
        {
            if (@event.AggregateId == Guid.Empty) return;

            var filter = Builders<NoSqlAccount>.Filter.Eq("_id", @event.AggregateId.ToString("N"));

            var documentResults = this._accountRepository.FindAsync(filter).Result;
            if (!documentResults.Any()) return;

            var updateEmail = Builders<NoSqlAccount>.Update.Set("Email", @event.Email);
            this._accountRepository.UpdateOneAsync(filter, updateEmail).Wait();
        }
    }
}
