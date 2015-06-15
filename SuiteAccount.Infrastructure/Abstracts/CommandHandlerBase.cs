using SuiteAccount.Infrastructure.Concretes;

namespace SuiteAccount.Infrastructure.Abstracts
{
    public abstract class CommandHandlerBase : ICommandHandler
    {
        public abstract void Apply(CommandBase command);
    }
}
