using System;
using MongoDB.Bson.Serialization.Attributes;

namespace SuiteAccount.NoSql.Persistence.Abstracts
{
    public abstract class MongoEntity : IMongoEntity
    {
        [BsonId]
        public String Id { get; set; }
    }
}
