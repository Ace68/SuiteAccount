using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuiteAccount.Shared.ValueObjects;
using SuiteAccount.Shared.ViewModels;

namespace SuiteAccount.Providers.Abstracts
{
    public interface IAccountProvider
    {
        void CreateAccount(AccountId accountId, string userName, string password);

        void UpdateEmail(AccountId accountId, string email);

        Task<Guid> VerifyAccountAsync(string userName, string password);

        Task<IReadOnlyCollection<AccountViewModel>> GetElencoAccountAsync();
    }
}
