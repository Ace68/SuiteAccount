using System;
using System.Data.Entity.Validation;
using System.Text;
using System.Threading.Tasks;

using SuiteAccount.Logging.Abstracts;
using SuiteAccount.QueryModel.Dtos;
using SuiteAccount.QueryModel.Persistence.Facade;
using SuiteAccount.QueryModel.Persistors;

namespace SuiteAccount.QueryModel.Persistence.Persistors
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly QueryModelFacade _queryModelFacade;
        private readonly ILogService _logService;

        public UnitOfWork(ILogService logService)
        {
            this._queryModelFacade = new QueryModelFacade();
            this._logService = logService;
        }

        #region Persistors
        private IPersistor<DtoAccount> _accountPersistor;
        public IPersistor<DtoAccount> AccountPersistor
        {
            get
            {
                return this._accountPersistor ??
                       (this._accountPersistor =
                           new Persistor<DtoAccount>(this._queryModelFacade, this._logService));
            }
        }
        #endregion

        public async Task CommitAsync()
        {
            using (var suiteContextTransaction = this._queryModelFacade.Database.BeginTransaction())
            {
                try
                {
                    await this._queryModelFacade.SaveChangesAsync();
                    suiteContextTransaction.Commit();
                }
                catch (DbEntityValidationException ex)
                {
                    var sb = new StringBuilder();
                    foreach (var failure in ex.EntityValidationErrors)
                    {
                        sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());

                        foreach (var error in failure.ValidationErrors)
                        {
                            sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                            sb.AppendLine();
                        }
                    }
                    suiteContextTransaction.Rollback();
                    this._logService.LoggerTrace(string.Format("UnitOfWOrk.CommitAsync: {0}", sb));
                    throw new Exception("UnitOfWork.Commit", ex);
                }
                catch (Exception ex)
                {
                    suiteContextTransaction.Rollback();
                    this._logService.ErrorTrace("UnitOfWOrk.CommitAsync", ex);
                    throw new Exception("UnitOfWork.Commit", ex);
                }
            }
        }

        #region Dispose
        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this._queryModelFacade.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
