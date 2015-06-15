using System;
using System.Web.Http;

using SuiteAccount.Infrastructure.Providers;
using SuiteAccount.Domain.Shared.Concretes;

namespace SuiteAccount.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IAccountProvider _accountProvider;

        public AccountController(IAccountProvider accountProvider)
        {
            this._accountProvider = accountProvider;
        }

        [HttpPost]
        public void CreateAccount()
        {
            const string username = "Ace68";
            const string password = "password";

            var accountId = new AccountId(new Guid());
            this._accountProvider.CreateAccount(accountId, username, password);
        }

        [HttpPost]
        public void UpdateEmail()
        {
            //20ed38d2-5058-4e78-9b57-7152920f3b05
            Guid userId;
            Guid.TryParse("2ffb528d-19ee-462d-b520-d36ea4283ec7", out userId);
            var accountId = new AccountId(userId);
            this._accountProvider.UpdateEmail(accountId, "a.acerbis@infocopy.it");
        }
    }
}
