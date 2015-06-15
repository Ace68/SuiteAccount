using CommonDomain.Persistence;

using Ninject;
using Ninject.Syntax;

using SuiteAccount.Domain.Commands;
using SuiteAccount.Domain.CommandsHandlers;
using SuiteAccount.Domain.Events;
using SuiteAccount.Infrastructure.Abstracts;
using SuiteAccount.QueryModel.Denormalizer.Abstracts;
using SuiteAccount.QueryModel.Denormalizer.DomainEventsHandlers;

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

            var account = new AccountCommandsHandler(repository);
            serviceBus.RegisterHandler<CreateAccount>(account.Handle);
            serviceBus.RegisterHandler<UpdateEmail>(account.Handle);
        }

        private static void RegisterEventHandlers(IResolutionRoot container, IServiceBus serviceBus,
            bool excludeMailHandler)
        {
            var accountDenormalizer = container.Get<IAccountDenormalizer>();
            var account = new AccountEventsHandler(accountDenormalizer);
            serviceBus.RegisterHandler<AccountCreated>(account.Handle);
            serviceBus.RegisterHandler<EmailUpdated>(account.Handle);
        }
    }
}
