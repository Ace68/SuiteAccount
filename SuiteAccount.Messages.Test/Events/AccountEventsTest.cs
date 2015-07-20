using System;
using NUnit.Framework;
using SuiteAccount.Messages.Events;
using SuiteAccount.Shared.ValueObjects;

namespace SuiteAccount.Messages.Test.Events
{
    [TestFixture]
    public class AccountEventsTest
    {
        [Test]
        public void AccountCreatedTest()
        {
            var accountId = new AccountId(Guid.NewGuid());
            var @event = new AccountCreated(accountId.Id, "username", "password");

            Assert.AreEqual(accountId.Id, @event.AggregateId);
            Assert.AreEqual("username", @event.UserName);
            Assert.AreEqual("password", @event.Password);
        }
    }
}
