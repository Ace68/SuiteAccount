using System;
using NUnit.Framework;
using SuiteAccount.Domain.Events;
using SuiteAccount.Domain.Shared.Concretes;

namespace SuiteAccount.Domain.Test.Events
{
    [TestFixture]
    public class AccountEventsTest
    {
        [Test]
        public void AccountCreated()
        {
            var accountId = new AccountId(Guid.NewGuid());
            var @event = new AccountCreated(accountId, "username", "password");

            Assert.AreEqual(accountId.Id, @event.AggregateId);
            Assert.AreEqual("username", @event.UserName);
            Assert.AreEqual("password", @event.Password);
        }
    }
}
