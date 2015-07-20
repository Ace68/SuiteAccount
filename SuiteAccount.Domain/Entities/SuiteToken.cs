using System;
using CommonDomain.Core;
using SuiteAccount.Messages.Events;
using SuiteAccount.Shared.Exceptions;
using SuiteAccount.Shared.ValueObjects;

namespace SuiteAccount.Domain.Entities
{
    public class SuiteToken : AggregateBase
    {
        public bool? IsAlive { get; private set; }
        public DateTime TokenGenerationDateUtc { get; private set; }
        public DateTime TokenExpirationDateUtc { get; private set; }
        public Guid AccountId { get; private set; }
        public Guid ApplicationId { get; private set; }

        #region ctor
        internal static SuiteToken CreateSuiteToken(TokenId tokenId, AccountId accountId, SuiteApplicationId applicationId,
            TimeSpan tokenDuration)
        {
            if (tokenId.Id == Guid.Empty)
                throw new ArgumentNullException("tokenId", DomainExceptions.TokenIdNullException);

            if (accountId.Id == Guid.Empty)
                throw new ArgumentNullException("accountId", DomainExceptions.AccountIdNullException);

            if (applicationId.Id == Guid.Empty)
                throw new ArgumentNullException("applicationId", DomainExceptions.ApplicatioIdNullException);

            var suiteToken = new SuiteToken(tokenId, accountId, applicationId, tokenDuration);

            return suiteToken;
        }

        private SuiteToken(TokenId tokenId, AccountId accountId, SuiteApplicationId applicationId,
            TimeSpan tokenDuration)
        {
            this.TokenGenerationDateUtc = DateTime.UtcNow;
            this.TokenExpirationDateUtc = this.TokenGenerationDateUtc.Add(tokenDuration);

            this.IsAlive = this.TokenExpirationDateUtc > this.TokenGenerationDateUtc;

            this.RaiseEvent(new SuiteTokenCreated(tokenId.Id, accountId.Id, applicationId.Id, tokenDuration,
                this.TokenGenerationDateUtc, this.TokenExpirationDateUtc, (bool) this.IsAlive));
        }

        private void Apply(SuiteTokenCreated @event)
        {
            this.Id = @event.AggregateId;
            this.AccountId = @event.AccountId;
            this.ApplicationId = @event.ApplicationId;
            this.IsAlive = @event.IsAlive;
        }
        #endregion
    }
}
