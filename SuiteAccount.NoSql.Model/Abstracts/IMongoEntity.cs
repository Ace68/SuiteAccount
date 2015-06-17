using System;
using MongoDB.Bson.Serialization.Attributes;

namespace SuiteAccount.NoSql.Model.Abstracts
{
    public interface IMongoEntity
    {
        [BsonId]
        String Id { get; set; }
    }
}
