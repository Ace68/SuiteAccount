using Ninject.Modules;
using SuiteAccount.Logging.Abstracts;
using SuiteAccount.Logging.Concretes;

namespace SuiteAccount.Infrastructures.Modules
{
    public class LoggingModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogService>().To<LogService>().InSingletonScope();
        }
    }
}
