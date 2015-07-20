using SuiteAccount.NoSql.Model.Abstracts;
using SuiteAccount.NoSql.Model.Documents;
using SuiteAccount.NoSql.Persistence.Abstracts;

namespace SuiteAccount.NoSql.Persistence.Persistors
{
    public class NoSqlUnitOfWork : INoSqlUnitOfWork
    {
        private IDocumentPersistor<NoSqlAccount> _noSqlAccountPersistor;
        public IDocumentPersistor<NoSqlAccount> NoSqlAccountPersistor
        {
            get
            {
                return this._noSqlAccountPersistor ??
                       ((this._noSqlAccountPersistor = new DocumentPersistor<NoSqlAccount>()));
            }
        }

        private IDocumentPersistor<NoSqlSuiteToken> _noSqlSuiteTokenPersistor;
        public IDocumentPersistor<NoSqlSuiteToken> NoSqlSuiteTokenPersistor
        {
            get
            {
                return this._noSqlSuiteTokenPersistor ??
                       (this._noSqlSuiteTokenPersistor = new DocumentPersistor<NoSqlSuiteToken>());
            }
        }

        private IDocumentPersistor<NoSqlSuiteApplication> _noSqlSuiteApplicationPersistor;
        public IDocumentPersistor<NoSqlSuiteApplication> NoSqlSuiteApplicationPersistor
        {
            get
            {
                return this._noSqlSuiteApplicationPersistor ??
                       (this._noSqlSuiteApplicationPersistor = new DocumentPersistor<NoSqlSuiteApplication>());
            }
        }
    }
}
