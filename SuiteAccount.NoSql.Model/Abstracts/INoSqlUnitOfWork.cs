
using SuiteAccount.NoSql.Model.Documents;

namespace SuiteAccount.NoSql.Model.Abstracts
{
    public interface INoSqlUnitOfWork
    {
        IDocumentPersistor<NoSqlAccount> NoSqlAccountPersistor { get; }
        IDocumentPersistor<NoSqlSuiteToken> NoSqlSuiteTokenPersistor { get; }
        IDocumentPersistor<NoSqlSuiteApplication> NoSqlSuiteApplicationPersistor { get; } 
    }
}
