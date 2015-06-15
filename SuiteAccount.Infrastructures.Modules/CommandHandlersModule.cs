using Ninject.Modules;
using SuiteAccount.Domain.Commands;
using SuiteAccount.Domain.CommandsHandlers;
using SuiteAccount.Infrastructure.Abstracts;

namespace SuiteAccount.Infrastructures.Modules
{
    public class CommandHandlersModule : NinjectModule
    {
        public override void Load()
        {
            #region Account
            Bind<IHandleCommand<CreateAccount>>().To<AccountCommandsHandler>().InSingletonScope();
            Bind<IHandleCommand<UpdateEmail>>().To<AccountCommandsHandler>().InSingletonScope();
            #endregion
        }
    }
}
