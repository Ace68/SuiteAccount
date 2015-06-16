using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace SuiteAccount.NoSql.Persistence.Abstracts
{
    public interface IDocumentRepository<TEntity> where TEntity : IMongoEntity
    {
        Task InsertOneAsync(TEntity documentToInsert);
        
        Task ReplaceOneAsync(FilterDefinition<TEntity> filter, TEntity documentToUpdate);
        
        Task DeleteOneAsync(FilterDefinition<TEntity> filter);

        Task<IList<TEntity>> FindAsync(FilterDefinition<TEntity> filter);

        Task UpdateOneAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition);

        Task<ReplaceOneResult> SaveAsync(TEntity documentToSave);
    }
}
