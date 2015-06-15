
using Ninject;

namespace SuiteAccount.Infrastructure.Abstracts
{
    public interface IModule
    {
        void Register(IKernel container);
    }
}
