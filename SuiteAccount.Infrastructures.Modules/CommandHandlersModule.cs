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
            this.Bind<IHandleCommand<CreateAccount>>().To<AccountCommandsHandler>().InSingletonScope();
            this.Bind<IHandleCommand<UpdateEmail>>().To<AccountCommandsHandler>().InSingletonScope();
            #endregion

            #region Token
            this.Bind<IHandleCommand<CreateSuiteToken>>().To<SuiteTokenCommandsHandler>().InSingletonScope();
            #endregion
        }
    }
}
