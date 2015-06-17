using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using SuiteAccount.SqlModel.Dtos;

namespace SuiteAccount.SqlModel.Persistors
{
    public interface IPersistor<TEntity> where TEntity : DtoBase
    {
        void Insert(TEntity entityToAdd);
        void Update(TEntity entityToUpdate);
        void Delete(Guid id);
        void Delete(TEntity entityToDelete);
        void RemoveRange(IEnumerable<TEntity> entitiesToRemove);

        Task<TEntity> GetByIdAsync(Guid id, string includeProperties = "");
        Task<IList<TEntity>> GetAllAsync(int pageIndex, int pageSize, string includeProperties = "");
        Task<IList<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
    }
}
