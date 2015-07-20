using System;
using System.Collections.Generic;

namespace SuiteAccount.Shared.ValueObjects
{
    public class SignInAccount : ValueObjectBase<SignInAccount>
    {
        public readonly Guid AccountGuid;
        public readonly Guid TokenGuid;
        public readonly string UserName;
        public readonly SuiteLogInStatus SuiteLogInStatus;

        public SignInAccount(Guid accountGuid, Guid tokenGuid, string username, SuiteLogInStatus suiteLogInStatus)
        {
            this.AccountGuid = accountGuid;
            this.TokenGuid = tokenGuid;
            this.UserName = username;
            this.SuiteLogInStatus = suiteLogInStatus;
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<Object>();
        }
    }
}
