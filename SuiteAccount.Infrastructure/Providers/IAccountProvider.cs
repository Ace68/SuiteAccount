using SuiteAccount.Domain.Shared.Concretes;

namespace SuiteAccount.Infrastructure.Providers
{
    public interface IAccountProvider
    {
        void CreateAccount(AccountId accountId, string userName, string password);
        void UpdateEmail(AccountId accountId, string email);
    }
}
