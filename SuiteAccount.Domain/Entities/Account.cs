using System;

using CommonDomain.Core;

using SuiteAccount.Domain.Events;
using SuiteAccount.Domain.Shared.Concretes;
using SuiteAccount.Domain.Shared.Exceptions;

namespace SuiteAccount.Domain.Entities
{
    public class Account : AggregateBase
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public EmailAddress Email { get; private set; }
        public bool IsApproved { get; private set; }
        public bool IsOnline { get; private set; }
        public bool IsLockedOut { get; private set; }
        public DateTime? CreationDate { get; private set; }

        protected Account()
        { }

        #region Create
        internal static Account CreateAccount(AccountId accountId, string userName, string password)
        {
            var account = new Account(accountId, userName, password);

            return account;
        }

        private Account(AccountId accountId, string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
                throw new ArgumentNullException("userName", DomainExceptions.UserNameNullException);

            if (String.IsNullOrEmpty(password))
                throw new ArgumentNullException("password", DomainExceptions.PasswordNullException);

            this.Id = accountId.Id;
            this.UserName = userName;
            this.Password = password;
            this.CreationDate = DateTime.UtcNow;
            this.IsApproved = false;
            this.IsOnline = false;
            this.IsLockedOut = true;

            this.RaiseEvent(new AccountCreated(accountId.Id, userName, password));
        }

        private void Apply(AccountCreated @event)
        {
            this.Id = @event.AggregateId;
            this.UserName = @event.UserName;
            this.Password = @event.Password;

            this.CreationDate = DateTime.UtcNow;
            this.IsApproved = false;
            this.IsOnline = false;
            this.IsLockedOut = true;
        }
        #endregion

        #region Email
        internal void UpdateEmail(AccountId accountId, EmailAddress emailAddress)
        {
            if (accountId.Id == Guid.Empty)
                throw new ArgumentNullException("accountId", DomainExceptions.AccountIdNullException);

            // Nessun controllo per emailAddress ... sto passando i dati tramite un ValueObject,
            // immutabile e con le sue regole di validazione, quindi assegno e sollevo l'evento ...
            this.Email = emailAddress;

            this.RaiseEvent(new EmailUpdated(accountId.Id, emailAddress.Email));
        }

        private void Apply(EmailUpdated @event)
        {
            this.Id = @event.AggregateId;
            this.Email = new EmailAddress(@event.Email);
        }
        #endregion
    }
}
