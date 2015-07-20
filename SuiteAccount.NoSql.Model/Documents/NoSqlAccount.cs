using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SuiteAccount.NoSql.Model.Abstracts;

namespace SuiteAccount.NoSql.Model.Documents
{
    [BsonIgnoreExtraElements]
    public class NoSqlAccount : MongoEntity
    {
        [BsonRepresentation(BsonType.String)]
        public string UserName { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string Password { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string Email { get; set; }
        public bool IsApproved { get; set; }
        public bool IsOnLine { get; set; }
        public bool IsLockedOut { get; set; }
        //[BsonDateTimeOptions(DateOnly = true)]
        public DateTime CreationDate { get; set; }
    }
}
