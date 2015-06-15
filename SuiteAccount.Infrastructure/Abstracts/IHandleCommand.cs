using SuiteAccount.Infrastructure.Concretes;

namespace SuiteAccount.Infrastructure.Abstracts
{
    public interface IHandleCommand<in T> where T : CommandBase
    {
        void Handle(T command);
    }
}
