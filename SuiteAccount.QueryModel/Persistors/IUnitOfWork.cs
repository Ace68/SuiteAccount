using System.Threading.Tasks;
using SuiteAccount.QueryModel.Dtos;

namespace SuiteAccount.QueryModel.Persistors
{
    public interface IUnitOfWork
    {
        IPersistor<DtoAccount> AccountPersistor { get; }

        Task CommitAsync();
    }
}
