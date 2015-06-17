using System;
using MongoDB.Bson.Serialization.Attributes;

namespace SuiteAccount.NoSql.Model.Abstracts
{
    public abstract class MongoEntity : IMongoEntity
    {
        [BsonId]
        public String Id { get; set; }
    }
}
