using System;
using System.Threading.Tasks;

namespace SuiteAccount.QueryModel.Denormalizer.Abstracts
{
    public interface IAccountDenormalizer
    {
        Task CreateAccountAsync(Guid accountId, string userName, string password);
        Task UpdateEmailAsync(Guid accountId, string email);
    }
}
