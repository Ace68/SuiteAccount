using System;
using System.Threading.Tasks;
using SuiteAccount.Messages.Commands;
using SuiteAccount.Infrastructure.Abstracts;
using SuiteAccount.NoSql.Services.Abstracts;
using SuiteAccount.Providers.Abstracts;
using SuiteAccount.Shared.ValueObjects;
using SuiteAccount.SqlModel.Services.Abstracts;

namespace SuiteAccount.Providers.Concretes
{
    public class SuiteTokenProvider : ISuiteTokenProvider
    {
        private readonly IServiceBus _serviceBus;
        private readonly INoSqlSuiteTokenService _noSqlSuiteTokenService;
        private readonly ISqlSuiteTokenService _sqlSuiteTokenService;

        public SuiteTokenProvider(IServiceBus serviceBus,
            INoSqlSuiteTokenService noSqlSuiteTokenService,
            ISqlSuiteTokenService sqlSuiteTokenService)
        {
            this._serviceBus = serviceBus;

            this._noSqlSuiteTokenService = noSqlSuiteTokenService;
            this._sqlSuiteTokenService = sqlSuiteTokenService;
        }

        public void CreateToken(TokenId tokenId, AccountId accountId, SuiteApplicationId applicationId)
        {
            var tokenDuration = new TimeSpan(0,
                Configuration.SuiteAccountConfiguration.SuiteTokenSection.Duration, 0);

            this._serviceBus.Send(new CreateSuiteToken(tokenId, accountId, applicationId, tokenDuration));
        }

        public async Task<Guid> VerifyTokenByAccountAsync(AccountId accountId)
        {
            var sqlTokenId = await this._sqlSuiteTokenService.VerifyTokenByAccountAsync(accountId);
            var noSqlTokenId = await this._noSqlSuiteTokenService.VerifyTokenByAccountAsync(accountId);

            return sqlTokenId == noSqlTokenId ? noSqlTokenId : Guid.Empty;
        }

        public async Task<bool> VerifiyTokenAsync(string tokenId)
        {
            return await this._noSqlSuiteTokenService.VerifiyTokenAsync(tokenId);
        }
    }
}
