using System;
using CommonDomain.Core;
using CommonDomain.Persistence;

namespace SuiteAccount.Domain.CommandsHandlers
{
    public abstract class CommandsHandler
    {
        protected readonly IRepository SuiteRepository;

        protected CommandsHandler(IRepository suiteRepository)
        {
            if (suiteRepository == null)
                throw new ArgumentNullException("suiteRepository");

            this.SuiteRepository = suiteRepository;
        }

        public TEntity Get<TEntity>(Guid id, IRepository repository) where TEntity : AggregateBase
        {
            var aggregate = repository.GetById<TEntity>(id);

            if (aggregate == null)
                throw new Exception(string.Format("{0} Not Found!", typeof(TEntity).Name));

            return aggregate;
        }
    }
}
