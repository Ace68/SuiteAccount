using System;
using System.Threading.Tasks;
using SuiteAccount.Shared.ValueObjects;

namespace SuiteAccount.SqlModel.Services.Abstracts
{
    public interface ISqlSuiteTokenService
    {
        Task CreateTokenAsync(TokenId tokenId, AccountId accountId, SuiteApplicationId applicationId,
            TimeSpan tokenDuration, DateTime tokenGenerationDateUtc, DateTime tokenExpirationDateUtc);

        Task<Guid> VerifyTokenByAccountAsync(AccountId accountId);
    }
}
