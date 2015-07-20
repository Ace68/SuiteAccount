using CommonDomain.Persistence;

using Ninject;
using Ninject.Syntax;
using SuiteAccount.Domain.CommandsHandlers;
using SuiteAccount.Messages.Commands;
using SuiteAccount.Messages.Events;
using SuiteAccount.Infrastructure.Abstracts;
using SuiteAccount.NoSql.Denormalizer.DomainEventsHandlers;
using SuiteAccount.NoSql.Services.Abstracts;
using SuiteAccount.SqlModel.Denormalizer.DomainEventsHandlers;
using SuiteAccount.SqlModel.Services.Abstracts;

namespace SuiteAccount.Infrastructures.Modules
{
    public class RegisterHandlerInBus
    {
        public static void Register(IKernel container, bool excludeMailHandler)
        {
            var serviceBus = container.Get<IServiceBus>();

            RegisterCommandHandlers(container, serviceBus);
            RegisterEventHandlers(container, serviceBus, excludeMailHandler);
        }

        private static void RegisterCommandHandlers(IResolutionRoot container, IServiceBus serviceBus)
        {
            var repository = container.Get<IRepository>();

            #region Account
            var account = new AccountCommandsHandler(repository);
            serviceBus.RegisterHandler<CreateAccount>(account.Handle);
            serviceBus.RegisterHandler<UpdateEmail>(account.Handle);
            #endregion

            #region SuiteToken
            var suiteToken = new SuiteTokenCommandsHandler(repository);
            serviceBus.RegisterHandler<CreateSuiteToken>(suiteToken.Handle);
            #endregion
        }

        private static void RegisterEventHandlers(IResolutionRoot container, IServiceBus serviceBus,
            bool excludeMailHandler)
        {
            #region Account
            var sqlAccountService = container.Get<ISqlAccountService>();
            var sqlAccount = new AccountEventsHandler(sqlAccountService);
            serviceBus.RegisterHandler<AccountCreated>(sqlAccount.Handle);
            serviceBus.RegisterHandler<EmailUpdated>(sqlAccount.Handle);

            var noSqlAccountService = container.Get<INoSqlAccountService>();
            var noSqlAccount = new NoSqlAccountEventsHandler(noSqlAccountService);
            serviceBus.RegisterHandler<AccountCreated>(noSqlAccount.Handle);
            serviceBus.RegisterHandler<EmailUpdated>(noSqlAccount.Handle);
            #endregion

            #region SuiteToken
            var sqlSuiteTokenService = container.Get<ISqlSuiteTokenService>();
            var sqlSuiteToken = new SuiteTokenEventsHandler(sqlSuiteTokenService);
            serviceBus.RegisterHandler<SuiteTokenCreated>(sqlSuiteToken.Handle);

            var noSqlSuiteTokenService = container.Get<INoSqlSuiteTokenService>();
            var noSqlSuiteToken = new NoSqlSuiteTokenEventsHandler(noSqlSuiteTokenService);
            serviceBus.RegisterHandler<SuiteTokenCreated>(noSqlSuiteToken.Handle);
            #endregion
        }
    }
}
