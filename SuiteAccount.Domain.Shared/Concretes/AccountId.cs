using System;

namespace SuiteAccount.Domain.Shared.Concretes
{
    public class AccountId
    {
        public Guid Id { get; private set; }

        public AccountId(Guid accountId)
        {
            if (accountId == Guid.Empty)
                accountId = Guid.NewGuid();

            this.Id = accountId;
        }
    }
}
