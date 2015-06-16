using System.Linq;
using System.Threading.Tasks;

using MongoDB.Driver;

using SuiteAccount.NoSql.Persistence.Abstracts;
using SuiteAccount.NoSql.Persistence.Entities;

namespace SuiteAccount.NoSql.Persistence.Repositories
{
    public class AccountRepository : DocumentRepository<NoSqlAccount>
    {
        public override async Task InsertOneAsync(NoSqlAccount documentToInsert)
        {
            var filter = Builders<NoSqlAccount>.Filter.Eq("userName", documentToInsert.UserName);
            
            var documentResults = await this.FindAsync(filter);
            if (documentResults.Any()) return;

            await this.MongoConnectionHandler.MongoCollection.InsertOneAsync(documentToInsert);
        }
    }
}
