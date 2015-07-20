using SuiteAccount.Infrastructure.Concretes;
using SuiteAccount.Shared.ValueObjects;

namespace SuiteAccount.Messages.Commands
{
    public class CreateAccount : CommandBase
    {
        public readonly string UserName;
        public readonly string Password;

        public CreateAccount(AccountId accountId, string userName, string password)
        {
            this.AggregateId = accountId.Id;
            this.UserName = userName;
            this.Password = password;
        }
    }

    public class UpdateEmail : CommandBase
    {
        public readonly string Email;

        public UpdateEmail(AccountId accountId, string email)
        {
            this.AggregateId = accountId.Id;
            this.Email = email;
        }
    }
}
