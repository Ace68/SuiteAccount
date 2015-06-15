using System;
using NUnit.Framework;
using SuiteAccount.Domain.Commands;
using SuiteAccount.Domain.Shared.Concretes;

namespace SuiteAccount.Domain.Test.Commands
{
    [TestFixture]
    public class AccountCommandsTest
    {
        [Test]
        public void CreateAccount()
        {
            var accountId = new AccountId(Guid.NewGuid());
            var command = new CreateAccount(accountId, "username", "password");

            Assert.AreEqual(accountId.Id, command.AggregateId);
            Assert.AreEqual("username", command.UserName);
        }
    }
}
