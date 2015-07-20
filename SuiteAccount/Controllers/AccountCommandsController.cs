using System;
using System.Threading.Tasks;
using System.Web.Http;

using SuiteAccount.Providers.Abstracts;
using SuiteAccount.Shared.ValueObjects;

namespace SuiteAccount.Controllers
{
    [RoutePrefix("api/AccountCommands")]
    public class AccountCommandsController : ApiController
    {
        private readonly IAccountProvider _accountProvider;
        private readonly ISuiteTokenProvider _suiteTokenProvider;

        public AccountCommandsController(IAccountProvider accountProvider,
            ISuiteTokenProvider suiteTokenProvider)
        {
            this._accountProvider = accountProvider;
            this._suiteTokenProvider = suiteTokenProvider;
        }

        [HttpPost]
        public void CreateAccount(string username, string password)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password)) return;

            var accountId = new AccountId(new Guid());
            this._accountProvider.CreateAccount(accountId, username, password);
        }

        [HttpPost]
        public void UpdateEmail(Guid userId, string email)
        {
            if (userId == Guid.Empty) return;

            var accountId = new AccountId(userId);
            this._accountProvider.UpdateEmail(accountId, email);
        }

        //#region Helpers
        //private IHttpActionResult GetErrorResult(IdentityResult result)
        //{
        //    if (result == null)
        //    {
        //        return InternalServerError();
        //    }

        //    if (result.Succeeded) return null;

        //    if (result.Errors != null)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            this.ModelState.AddModelError("", error);
        //        }
        //    }

        //    if (this.ModelState.IsValid)
        //    {
        //        // No ModelState errors are available to send, so just return an empty BadRequest.
        //        return this.BadRequest();
        //    }

        //    return this.BadRequest(this.ModelState);
        //}
        //#endregion

        #region Test
        [HttpPost]
        public void CreateAccountTest()
        {
            const string username = "Alberto";
            const string password = "Ace68";

            var accountId = new AccountId(new Guid());
            this._accountProvider.CreateAccount(accountId, username, password);
        }

        [HttpPost]
        public async Task UpdateEmailTestAsync()
        {
            var accountGuid = await this._accountProvider.VerifyAccountAsync("User", "InfoCopy");
            if (accountGuid == Guid.Empty) return;

            var accountId = new AccountId(accountGuid);
            this._accountProvider.UpdateEmail(accountId, "u.user@infocopy.it");
        }

        [HttpGet]
        public async Task<SignInAccount> VerifyAccountTestAsync()
        {
            var accountGuid = await this._accountProvider.VerifyAccountAsync("Admin", "InfoCopy");

            if (accountGuid == Guid.Empty) 
                return new SignInAccount(Guid.Empty, Guid.Empty, "Admin", SuiteLogInStatus.Failure);

            var tokenId = new TokenId(Guid.NewGuid());
            var accountId = new AccountId(accountGuid);
            
            Guid applicationGuid;
            Guid.TryParse("b9590d6b-18fa-48fd-84f9-c3c41f79cfbd", out applicationGuid);
            var applicationId = new SuiteApplicationId(applicationGuid);

            var tokenGuid = await this._suiteTokenProvider.VerifyTokenByAccountAsync(accountId);

            if (tokenGuid != Guid.Empty) 
                return new SignInAccount(accountGuid, tokenGuid, "Admin", SuiteLogInStatus.Success);

            this._suiteTokenProvider.CreateToken(tokenId, accountId, applicationId);
            tokenGuid = await this._suiteTokenProvider.VerifyTokenByAccountAsync(accountId);

            return new SignInAccount(accountGuid, tokenGuid, "Admin", SuiteLogInStatus.Success);
        }
        #endregion
    }
}
