﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using SuiteAccount.Logging.Abstracts;
using SuiteAccount.SqlModel.Dtos;
using SuiteAccount.SqlModel.Persistence.Facade;
using SuiteAccount.SqlModel.Persistors;

namespace SuiteAccount.SqlModel.Persistence.Persistors
{
    internal class Persistor<TEntity> : IPersistor<TEntity> where TEntity : DtoBase
    {
        private readonly ILogService _logService;
        private readonly SqlModelFacade _sqlModelFacade;
        internal DbSet<TEntity> DbSet;

        public Persistor(SqlModelFacade sqlModelFacade,
            ILogService logService)
        {
            this._logService = logService;
            this._sqlModelFacade = sqlModelFacade;
            this.DbSet = sqlModelFacade.Set<TEntity>();
        }

        public void Insert(TEntity entityToAdd)
        {
            this.DbSet.Add(entityToAdd);
        }

        public void Update(TEntity entityToUpdate)
        {
            this.DbSet.Attach(entityToUpdate);
            this._sqlModelFacade.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var entityToDelete = this.DbSet.Find(id);

            if (entityToDelete != null)
                Delete(entityToDelete);
        }

        public void Delete(TEntity entityToDelete)
        {
            if (this._sqlModelFacade.Entry(entityToDelete).State == EntityState.Detached)
            {
                this.DbSet.Attach(entityToDelete);
            }

            this.DbSet.Remove(entityToDelete);
        }

        public void RemoveRange(IEnumerable<TEntity> entitiesToRemove)
        {
            this.DbSet.RemoveRange(entitiesToRemove);
        }

        public async Task<TEntity> GetByIdAsync(Guid id, string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<IList<TEntity>> GetAllAsync(int pageIndex, int pageSize, string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

            query = includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (pageSize > 0)
                query = query.OrderBy(q => q.Id).Skip(pageIndex * pageSize).Take(pageSize);

            try
            {
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                this._logService.ErrorTrace(string.Format("Persistor.GetAllAsync-{0}", this.DbSet), ex);
                return new List<TEntity>();
            }
        }

        public async Task<IList<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

            query = includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (filter != null)
                query = query.Where(filter);

            try
            {
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                this._logService.ErrorTrace(string.Format("Persistor.QueryAsync-{0}", this.DbSet), ex);
                return new List<TEntity>();
            }
        }
    }
}
