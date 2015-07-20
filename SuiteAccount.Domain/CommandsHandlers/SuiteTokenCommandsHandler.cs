using System;

using CommonDomain.Persistence;

using SuiteAccount.Messages.Commands;
using SuiteAccount.Domain.Entities;
using SuiteAccount.Infrastructure.Abstracts;
using SuiteAccount.Shared.ValueObjects;

namespace SuiteAccount.Domain.CommandsHandlers
{
    public class SuiteTokenCommandsHandler : CommandsHandler,
        IHandleCommand<CreateSuiteToken>
    {
        public SuiteTokenCommandsHandler(IRepository repository)
            :base(repository)
        { }

        public void Handle(CreateSuiteToken command)
        {
            var tokenId = new TokenId(command.AggregateId);
            var accountId = new AccountId(command.AccountId);
            var applicationId = new SuiteApplicationId(command.ApplicationId);

            var suiteToken = SuiteToken.CreateSuiteToken(tokenId, accountId, applicationId, command.TokenDuration);

            this.SuiteRepository.Save(suiteToken, Guid.NewGuid(), d => { });
        }
    }
}
