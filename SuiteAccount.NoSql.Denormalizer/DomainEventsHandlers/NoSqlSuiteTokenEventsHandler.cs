using System;
using SuiteAccount.Messages.Events;
using SuiteAccount.Infrastructure.Abstracts;
using SuiteAccount.NoSql.Services.Abstracts;
using SuiteAccount.Shared.ValueObjects;

namespace SuiteAccount.NoSql.Denormalizer.DomainEventsHandlers
{
    public class NoSqlSuiteTokenEventsHandler :
        IEventHandler<SuiteTokenCreated>
    {
        private readonly INoSqlSuiteTokenService _noSqlSuiteTokenService;

        public NoSqlSuiteTokenEventsHandler(INoSqlSuiteTokenService noSqlSuiteTokenService)
        {
            this._noSqlSuiteTokenService = noSqlSuiteTokenService;
        }

        public void Handle(SuiteTokenCreated @event)
        {
            if (@event.AggregateId == Guid.Empty) return;

            var tokenId = new TokenId(@event.AggregateId);
            var accouId = new AccountId(@event.AccountId);
            var applicationId = new SuiteApplicationId(@event.ApplicationId);

            this._noSqlSuiteTokenService.CreateSuiteTokenAsync(tokenId, accouId, applicationId, @event.TokenDuration,
                @event.TokenGenerationDateUtc, @event.TokenExpirationDateUtc);
        }
    }
}
