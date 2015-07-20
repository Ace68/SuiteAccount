using System;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Driver;

using SuiteAccount.NoSql.Model.Abstracts;
using SuiteAccount.NoSql.Model.Documents;
using SuiteAccount.NoSql.Services.Abstracts;
using SuiteAccount.Shared.ValueObjects;

namespace SuiteAccount.NoSql.Services.Concretes
{
    public class NoSqlSuiteTokenService : INoSqlSuiteTokenService
    {
        private readonly INoSqlUnitOfWork _noSqlUnitOfWork;

        public NoSqlSuiteTokenService(INoSqlUnitOfWork noSqlUnitOfWork)
        {
            this._noSqlUnitOfWork = noSqlUnitOfWork;
        }

        public async Task CreateSuiteTokenAsync(TokenId tokenId, AccountId accountId, SuiteApplicationId applicationId,
            TimeSpan tokenDuration, DateTime tokenGenerationDateUtc, DateTime tokenExpirationDateUtc)
        {
            var suiteToken = new NoSqlSuiteToken
            {
                Id = tokenId.Id.ToString("N"),
                AccountId = accountId.Id.ToString("N"),
                AccountName = string.Empty,
                ApplicationId = applicationId.Id.ToString("N"),
                ApplicationName = string.Empty,
                IsAlive = false,
                TokenDuration = tokenDuration.Minutes,
                TokenGenerationDateUtc = tokenGenerationDateUtc,
                TokenExpirationDateUtc = tokenExpirationDateUtc
            };

            if (tokenExpirationDateUtc > tokenGenerationDateUtc)
                suiteToken.IsAlive = true;

            await this._noSqlUnitOfWork.NoSqlSuiteTokenPersistor.InsertOneAsync(suiteToken);
        }

        public async Task<Guid> VerifyTokenByAccountAsync(AccountId accountId)
        {
            var filter = Builders<NoSqlSuiteToken>.Filter.Eq("AccountId", accountId.Id.ToString("N")) &
                        Builders<NoSqlSuiteToken>.Filter.Eq("IsAlive", true);

            var documentResults = await this._noSqlUnitOfWork.NoSqlSuiteTokenPersistor.FindAsync(filter);

            if (!documentResults.Any()) return Guid.Empty;

            foreach (
                var noSqlSuiteToken in
                    documentResults.TakeWhile(
                        noSqlSuiteToken => noSqlSuiteToken.TokenExpirationDateUtc < DateTime.UtcNow))
            {
                noSqlSuiteToken.IsAlive = false;
                filter = Builders<NoSqlSuiteToken>.Filter.Eq("_id", noSqlSuiteToken.Id);
                var updateToken = Builders<NoSqlSuiteToken>.Update.Set("IsAlive", false);
                await this._noSqlUnitOfWork.NoSqlSuiteTokenPersistor.UpdateOneAsync(filter, updateToken);
            }

            var tokenAlive = documentResults.First();
            if ((bool)!tokenAlive.IsAlive) return Guid.Empty;

            Guid tokenGuid;
            Guid.TryParse(tokenAlive.Id, out tokenGuid);

            return tokenGuid;
        }

        public async Task<bool> VerifiyTokenAsync(string tokenId)
        {
            var filter = Builders<NoSqlSuiteToken>.Filter.Eq("_id", tokenId);

            var documentResults = await this._noSqlUnitOfWork.NoSqlSuiteTokenPersistor.FindAsync(filter);
            if (!documentResults.Any()) return false;

            foreach (
                var noSqlSuiteToken in
                    documentResults.TakeWhile(
                        noSqlSuiteToken => noSqlSuiteToken.TokenExpirationDateUtc < DateTime.UtcNow))
            {
                noSqlSuiteToken.IsAlive = false;
                filter = Builders<NoSqlSuiteToken>.Filter.Eq("_id", noSqlSuiteToken.Id);
                var updateToken = Builders<NoSqlSuiteToken>.Update.Set("IsAlive", false);
                await this._noSqlUnitOfWork.NoSqlSuiteTokenPersistor.UpdateOneAsync(filter, updateToken);
            }

            var tokenAlive = documentResults.First();
            return !(bool)!tokenAlive.IsAlive;
        }
    }
}
