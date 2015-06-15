using System;
using CommonDomain.Persistence;

using SuiteAccount.Domain.Commands;
using SuiteAccount.Domain.Entities;
using SuiteAccount.Infrastructure.Abstracts;
using SuiteAccount.Domain.Shared.Concretes;

namespace SuiteAccount.Domain.CommandsHandlers
{
    public class AccountCommandsHandler : CommandsHandler,
        IHandleCommand<CreateAccount>,
        IHandleCommand<UpdateEmail>
    {
        public AccountCommandsHandler(IRepository repository)
            :base(repository)
        { }

        public void Handle(CreateAccount command)
        {
            var accountId = new AccountId(command.AggregateId);
            var account = Account.CreateAccount(accountId, command.UserName, command.Password);

            this.SuiteRepository.Save(account, Guid.NewGuid(), d => { });
        }

        public void Handle(UpdateEmail command)
        {
            var account = this.Get<Account>(command.AggregateId, this.SuiteRepository);
            var accountId = new AccountId(account.Id);
            var emailAddress = new EmailAddress(command.Email);
            account.UpdateEmail(accountId, emailAddress);
            this.SuiteRepository.Save(account, Guid.NewGuid(), d => { });
        }
    }
}
