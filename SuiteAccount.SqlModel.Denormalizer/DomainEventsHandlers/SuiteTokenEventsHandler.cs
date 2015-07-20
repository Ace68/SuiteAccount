using System;
using SuiteAccount.Messages.Events;
using SuiteAccount.Infrastructure.Abstracts;
using SuiteAccount.Shared.ValueObjects;
using SuiteAccount.SqlModel.Services.Abstracts;

namespace SuiteAccount.SqlModel.Denormalizer.DomainEventsHandlers
{
    public class SuiteTokenEventsHandler :
        IEventHandler<SuiteTokenCreated>
    {
        private readonly ISqlSuiteTokenService _sqlSuiteTokenService;

        public SuiteTokenEventsHandler(ISqlSuiteTokenService sqlSuiteTokenService)
        {
            this._sqlSuiteTokenService = sqlSuiteTokenService;
        }

        public void Handle(SuiteTokenCreated @event)
        {
            if (@event.AggregateId == Guid.Empty) return;

            var tokenId = new TokenId(@event.AggregateId);
            var accountId = new AccountId(@event.AccountId);
            var applicationId = new SuiteApplicationId(@event.ApplicationId);

            this._sqlSuiteTokenService.CreateTokenAsync(tokenId, accountId, applicationId, @event.TokenDuration,
                @event.TokenGenerationDateUtc, @event.TokenExpirationDateUtc);
        }
    }
}
