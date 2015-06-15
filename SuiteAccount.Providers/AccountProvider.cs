using SuiteAccount.Domain.Commands;
using SuiteAccount.Infrastructure.Abstracts;
using SuiteAccount.Infrastructure.Providers;
using SuiteAccount.Domain.Shared.Concretes;

namespace SuiteAccount.Providers
{
    public class AccountProvider : IAccountProvider
    {
        private readonly IServiceBus _serviceBus;

        public AccountProvider(IServiceBus serviceBus)
        {
            this._serviceBus = serviceBus;
        }

        public void CreateAccount(AccountId accountId, string userName, string password)
        {
            this._serviceBus.Send(new CreateAccount(accountId, userName, password));
        }

        public void UpdateEmail(AccountId accountId, string email)
        {
            this._serviceBus.Send(new UpdateEmail(accountId, email));
        }
    }
}
