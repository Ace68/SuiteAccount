using System;
using System.Collections.Generic;

namespace SuiteAccount.Shared.ValueObjects
{
    public class SuiteApplicationId : ValueObjectBase<SuiteApplicationId>
    {
        public readonly Guid Id;

        public SuiteApplicationId(Guid applicationGuid)
        {
            this.Id = applicationGuid;
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<Object>();
        }
    }
}
