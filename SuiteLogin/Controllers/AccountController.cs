using System;
using System.Web.Http;

using Microsoft.AspNet.Identity;

using SuiteAccount.Infrastructure.Providers;
using SuiteAccount.Shared.Concretes;

namespace SuiteLogin.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IAccountProvider _accountProvider;

        public AccountController(IAccountProvider accountProvider)
        {
            this._accountProvider = accountProvider;
        }

        #region Test
        [AllowAnonymous]
        [HttpPost]
        public void CreateAccountTest()
        {
            const string username = "Test";
            const string password = "password";

            var accountId = new AccountId(new Guid());
            this._accountProvider.CreateAccount(accountId, username, password);
        }

        [AllowAnonymous]
        [HttpPost]
        public void UpdateEmailTest()
        {
            Guid userId;
            Guid.TryParse("72118984-96d4-4920-b23c-ee7d99609532", out userId);
            var accountId = new AccountId(userId);
            this._accountProvider.UpdateEmail(accountId, "test.second@infocopy.it");
        }
        #endregion

        #region Helpers
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (result.Succeeded) return null;

            if (result.Errors != null)
            {
                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError("", error);
                }
            }

            if (this.ModelState.IsValid)
            {
                // No ModelState errors are available to send, so just return an empty BadRequest.
                return this.BadRequest();
            }

            return this.BadRequest(this.ModelState);
        }
        #endregion
    }
}
