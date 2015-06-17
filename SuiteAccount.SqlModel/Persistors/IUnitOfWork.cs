using System.Threading.Tasks;
using SuiteAccount.SqlModel.Dtos;

namespace SuiteAccount.SqlModel.Persistors
{
    public interface IUnitOfWork
    {
        IPersistor<DtoAccount> AccountPersistor { get; }

        Task CommitAsync();
    }
}
