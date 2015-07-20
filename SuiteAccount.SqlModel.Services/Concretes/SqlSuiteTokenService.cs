using System;
using System.Linq;
using System.Threading.Tasks;
using SuiteAccount.Shared.Exceptions;
using SuiteAccount.Shared.ValueObjects;
using SuiteAccount.SqlModel.Dtos;
using SuiteAccount.SqlModel.Persistors;
using SuiteAccount.SqlModel.Services.Abstracts;

namespace SuiteAccount.SqlModel.Services.Concretes
{
    public class SqlSuiteTokenService : ISqlSuiteTokenService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SqlSuiteTokenService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task CreateTokenAsync(TokenId tokenId, AccountId accountId, SuiteApplicationId applicationId,
            TimeSpan tokenDuration, DateTime tokenGenerationDateUtc, DateTime tokenExpirationDateUtc)
        {
            if (tokenId.Id == Guid.Empty)
                throw new ArgumentNullException("tokenId", DomainExceptions.TokenIdNullException);

            if (applicationId.Id == Guid.Empty)
                throw new ArgumentNullException("applicationId", DomainExceptions.ApplicatioIdNullException);

            if (accountId.Id == Guid.Empty)
                throw new ArgumentNullException("accountId", DomainExceptions.ApplicatioIdNullException);

            var chkToken = await this._unitOfWork.SuiteTokenPersistor.GetByIdAsync(tokenId.Id);
            if (chkToken != null) return;

            var suiteToken = new DtoSuiteToken
            {
                Id = tokenId.Id,
                AccountId = accountId.Id,
                AccountName = string.Empty,
                ApplicationId = applicationId.Id,
                ApplicationName = string.Empty,
                TokenDuration = tokenDuration.Minutes,
                TokenGenerationDateUtc = tokenGenerationDateUtc,
                TokenExpirationDateUtc = tokenExpirationDateUtc,
                IsAlive = false
            };

            if (suiteToken.TokenExpirationDateUtc > suiteToken.TokenGenerationDateUtc)
                suiteToken.IsAlive = true;

            this._unitOfWork.SuiteTokenPersistor.Insert(suiteToken);
            await this._unitOfWork.CommitAsync();
        }

        public async Task<Guid> VerifyTokenByAccountAsync(AccountId accountId)
        {
            var tokenResults =
                await
                    this._unitOfWork.SuiteTokenPersistor.QueryAsync(
                        t => t.AccountId == accountId.Id && t.IsAlive == true);

            if (!tokenResults.Any()) return Guid.Empty;

            foreach (var dtoSuiteToken in tokenResults)
            {
                if (dtoSuiteToken.TokenExpirationDateUtc > DateTime.UtcNow) break;

                dtoSuiteToken.IsAlive = false;
                this._unitOfWork.SuiteTokenPersistor.Update(dtoSuiteToken);
            }

            await this._unitOfWork.CommitAsync();

            var tokenAlive = tokenResults.First();
            if ((bool)!tokenAlive.IsAlive) return Guid.Empty;

            return tokenAlive.Id;
        }
    }
}
