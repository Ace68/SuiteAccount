using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SuiteAccount.Shared.Exceptions;

namespace SuiteAccount.Shared.ValueObjects
{
    public class EmailAddress : ValueObjectBase<EmailAddress>
    {
        public string Email { get; private set; }

        private const string EmailPattern =
            @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public EmailAddress(string email)
        {
            var match = Regex.Match(email, EmailPattern);

            if (!match.Success)
                throw new ArgumentNullException("email", DomainExceptions.EmailNullException);

            this.Email = email;
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<Object>();
        }
    }
}
