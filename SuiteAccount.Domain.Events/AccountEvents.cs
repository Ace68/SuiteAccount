using System;
using SuiteAccount.Domain.Shared.Concretes;
using SuiteAccount.Infrastructure.Concretes;

namespace SuiteAccount.Domain.Events
{
    public class AccountCreated : EventBase
    {
        public readonly string UserName;
        public readonly string Password;

        public AccountCreated(Guid userId, string userName, string password)
        {
            this.AggregateId = userId;
            this.UserName = userName;
            this.Password = password;
        }
    }

    public class EmailUpdated : EventBase
    {
        public readonly string Email;

        public EmailUpdated(Guid userId, string email)
        {
            this.AggregateId = userId;
            this.Email = email;
        }
    }
}
