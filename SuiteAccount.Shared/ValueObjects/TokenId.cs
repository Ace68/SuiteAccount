using System;
using System.Collections.Generic;

namespace SuiteAccount.Shared.ValueObjects
{
    public class TokenId : ValueObjectBase<TokenId>
    {
        public readonly Guid Id;

        public TokenId(Guid tokenGuid)
        {
            this.Id = tokenGuid;
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<Object>();
        }
    }
}
