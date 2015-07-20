using System;
using System.Threading.Tasks;
using SuiteAccount.Shared.ValueObjects;

namespace SuiteAccount.Providers.Abstracts
{
    public interface ISuiteTokenProvider
    {
        void CreateToken(TokenId tokenId, AccountId accountId, SuiteApplicationId applicationId);

        Task<Guid> VerifyTokenByAccountAsync(AccountId accountId);

        Task<bool> VerifiyTokenAsync(string tokenId);
    }
}
