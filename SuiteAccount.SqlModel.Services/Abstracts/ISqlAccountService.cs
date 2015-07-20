using System;
using System.Threading.Tasks;

namespace SuiteAccount.SqlModel.Services.Abstracts
{
    public interface ISqlAccountService
    {
        Task CreateAccountAsync(Guid accountId, string userName, string password);
        Task UpdateEmailAsync(Guid accountId, string email);
        Task<Guid> VerifyAccountAsync(string userName, string password);
    }
}
