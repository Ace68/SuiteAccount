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

        #region Test
        [HttpPost]
        public void CreateAccountTest()
        {
            const string username = "Test";
            const string password = "password";

            var accountId = new AccountId(new Guid());
            this._accountProvider.CreateAccount(accountId, username, password);
        }

        [HttpPost]
        public void UpdateEmailTest()
        {
            Guid userId;
            Guid.TryParse("a1cdd601-318b-4b80-ace4-98f7538f86c1", out userId);
            var accountId = new AccountId(userId);
            this._accountProvider.UpdateEmail(accountId, "test.second@infocopy.it");
        }
        #endregion
    }
}
