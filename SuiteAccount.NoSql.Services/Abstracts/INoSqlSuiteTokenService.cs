using System;
using System.Threading.Tasks;
using SuiteAccount.Shared.ValueObjects;

namespace SuiteAccount.NoSql.Services.Abstracts
{
    public interface INoSqlSuiteTokenService
    {
        Task CreateSuiteTokenAsync(TokenId tokenId, AccountId accountId, SuiteApplicationId applicationId,
            TimeSpan tokenDuration, DateTime tokenGenerationDateUtc, DateTime tokenExpirationDateUtc);

        Task<Guid> VerifyTokenByAccountAsync(AccountId accountId);

        Task<bool> VerifiyTokenAsync(string tokenId);
    }
}
