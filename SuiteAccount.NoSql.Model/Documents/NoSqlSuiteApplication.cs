using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SuiteAccount.NoSql.Model.Abstracts;

namespace SuiteAccount.NoSql.Model.Documents
{
    public class NoSqlSuiteApplication : MongoEntity
    {
        [BsonRepresentation(BsonType.String)]
        public string ApplicationName { get; set; }
    }
}
