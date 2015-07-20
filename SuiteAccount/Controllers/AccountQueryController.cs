using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using SuiteAccount.Providers.Abstracts;
using SuiteAccount.Shared.ValueObjects;
using SuiteAccount.Shared.ViewModels;

namespace SuiteAccount.Controllers
{
    [RoutePrefix("api/AccountQuery")]
    public class AccountQueryController : ApiController
    {
        private readonly IAccountProvider _accountProvider;
        private readonly ISuiteTokenProvider _suiteTokenProvider;

        public AccountQueryController(IAccountProvider accountProvider,
            ISuiteTokenProvider suiteTokenProvider)
        {
            this._accountProvider = accountProvider;
            this._suiteTokenProvider = suiteTokenProvider;
        }

        [HttpGet]
        public async Task<SignInAccount> VerifyAccountAsync(string username, string password, string applicationName)
        {
            var accountGuid = await this._accountProvider.VerifyAccountAsync(username, password);

            if (accountGuid == Guid.Empty)
                return new SignInAccount(Guid.Empty, Guid.Empty, username, SuiteLogInStatus.Failure);

            var accountId = new AccountId(accountGuid);

            Guid applicationGuid;
            Guid.TryParse("b9590d6b-18fa-48fd-84f9-c3c41f79cfbd", out applicationGuid);
            var applicationId = new SuiteApplicationId(applicationGuid);

            var tokenGuid = await this._suiteTokenProvider.VerifyTokenByAccountAsync(accountId);

            if (tokenGuid != Guid.Empty)
                return new SignInAccount(accountGuid, tokenGuid, username, SuiteLogInStatus.Success);

            var tokenId = new TokenId(Guid.NewGuid());
            this._suiteTokenProvider.CreateToken(tokenId, accountId, applicationId);
            tokenGuid = await this._suiteTokenProvider.VerifyTokenByAccountAsync(accountId);

            return new SignInAccount(accountGuid, tokenGuid, username, SuiteLogInStatus.Success);
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<AccountViewModel>> GetElencoAccountAsync()
        {
            return await this._accountProvider.GetElencoAccountAsync();
        }
    }
}
