using System;
using SuiteAccount.NoSql.Model.Abstracts;

namespace SuiteAccount.NoSql.Model.Documents
{
    public class NoSqlSuiteToken : MongoEntity
    {
        public bool? IsAlive { get; set; }
        public DateTime TokenGenerationDateUtc { get; set; }
        public DateTime TokenExpirationDateUtc { get; set; }
        public int? TokenDuration { get; set; }
        public String AccountId { get; set; }
        public string AccountName { get; set; }
        public String ApplicationId { get; set; }
        public string ApplicationName { get; set; }
    }
}
