using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

using SuiteAccount.NoSql.Model.Abstracts;
using SuiteAccount.NoSql.Persistence.Repositories;

namespace SuiteAccount.NoSql.Persistence.Abstracts
{
    public abstract class DocumentRepository<TEntity> : IDocumentRepository<TEntity> where TEntity : IMongoEntity
    {
        protected readonly MongoConnectionHandler<TEntity> MongoConnectionHandler;

        protected DocumentRepository()
        {
            MongoConnectionHandler = new MongoConnectionHandler<TEntity>();
        }

        public virtual async Task InsertOneAsync(TEntity documentToInsert)
        {
            await this.MongoConnectionHandler.MongoCollection.InsertOneAsync(documentToInsert);
        }

        public async Task ReplaceOneAsync(FilterDefinition<TEntity> filter, TEntity documentToUpdate)
        {
            await this.MongoConnectionHandler.MongoCollection.ReplaceOneAsync(filter, documentToUpdate);
        }

        public virtual async Task DeleteOneAsync(FilterDefinition<TEntity> filter)
        {
            await this.MongoConnectionHandler.MongoCollection.DeleteOneAsync(filter);
        }

        public virtual async Task<IList<TEntity>> FindAsync(FilterDefinition<TEntity> filter)
        {
            var listaDocumenti = new List<TEntity>();
            
            using (var cursor = await this.MongoConnectionHandler.MongoCollection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    listaDocumenti.AddRange(batch);
                }
            }

            return listaDocumenti;
        }

        public virtual async Task UpdateOneAsync(FilterDefinition<TEntity> filter,
            UpdateDefinition<TEntity> updateDefinition)
        {
            await this.MongoConnectionHandler.MongoCollection.UpdateOneAsync(filter, updateDefinition);
        }

        public virtual async Task<ReplaceOneResult> SaveAsync(TEntity documentToSave)
        {
            var replaceOneResult = await this.MongoConnectionHandler.MongoCollection.ReplaceOneAsync(
                doc => doc.Id == documentToSave.Id,
                documentToSave,
                new UpdateOptions {IsUpsert = true});

            return replaceOneResult;
        }
    }
}
