using System;
using System.Collections.Generic;

namespace SuiteAccount.Shared.ValueObjects
{
    public class AccountId : ValueObjectBase<AccountId>
    {
        public Guid Id { get; private set; }

        public AccountId(Guid accountId)
        {
            if (accountId == Guid.Empty)
                accountId = Guid.NewGuid();

            this.Id = accountId;
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<Object>();
        }
    }
}
