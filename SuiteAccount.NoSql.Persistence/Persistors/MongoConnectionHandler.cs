// http://www.drdobbs.com/database/mongodb-with-c-deep-dive/240152181?pgno=2
// I'm using the new MongoDb .NET Api.
using MongoDB.Driver;

using SuiteAccount.Configuration;
using SuiteAccount.NoSql.Model.Abstracts;

namespace SuiteAccount.NoSql.Persistence.Persistors
{
    public class MongoConnectionHandler<TEntity> where TEntity : IMongoEntity
    {
        public IMongoCollection<TEntity> MongoCollection { get; private set; }

        public MongoConnectionHandler()
        {
            // Get a thread-safe client object by using a connection string
            var mongoClient = new MongoClient(SuiteAccountConfiguration.MongoDbSection.Uri);

            // Get a reference to a database object from the Mongo client object
            var mongoDatabase = mongoClient.GetDatabase("SuiteAccount");

            // Get a reference to the collection object from the Mongo database object
            // The collection name is the type converted to lowercase + "s"

            var typeName = typeof (TEntity).Name.ToLower();
            typeName = typeName.Replace("nosql", "");

            MongoCollection = mongoDatabase.GetCollection<TEntity>(typeName + "s");
        }
    }
}
