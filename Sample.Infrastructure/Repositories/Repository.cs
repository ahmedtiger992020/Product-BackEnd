using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Sample.Core.Interfaces.Repository;
using Sample.Infrastructure.Context;
using Sample.SharedKernal.Exceptions;
using static Sample.SharedKernal.Enums.SharedKernelEnums;

namespace Sample.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class //BaseEntity
    {
        /// <summary>
        /// Defines the context
        /// </summary>
        private readonly SampleContext context;

        /// <summary>
        /// Defines the entities
        /// </summary>
        private DbSet<T> entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="ElectricityCorrespondenceContext"/></param> 
        public Repository(SampleContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        #region Insert
        public T Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            return entities.Add(entity).Entity;
        }
        public async Task<T> InsertAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            return (await entities.AddAsync(entity)).Entity;
        }
        public void BulkInsert(List<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");
            context.AddRangeAsync(entities);
        }
        #endregion

        #region Update
        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            entities.Update(entity);
        }

        #endregion

        #region Delete


        public void HardDelete(T entity)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");
            entities.Remove(entity);
        }
        #endregion

        #region Retreive

        #region GetById
        public async Task<T> GetById(int Id)
        {
            T record = await entities.FindAsync(Id);

            return record;
        }
        #endregion

        #region GetList
        public async Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = entities;
            if (filter != null)
                query = query.Where(filter);

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.AsNoTracking().ToListAsync();
        }
        public async Task<List<T>> GetWhereAsync<TKey>(Expression<Func<T, bool>> filter = null, string includeProperties = "", Expression<Func<T, TKey>> sortingExpression = null, SortDirection sortDir = SortDirection.Ascending)
        {
            IQueryable<T> query = entities;

            if (filter != null)
                query = query.Where(filter);

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (sortingExpression != null)
            {
                switch (sortDir)
                {
                    case SortDirection.Ascending:
                        query = query.OrderBy<T, TKey>(sortingExpression); break;
                    case SortDirection.Descending:
                        query = query.OrderByDescending<T, TKey>(sortingExpression); break;
                }
            }
            return await query.AsNoTracking().ToListAsync();
        }
        #endregion

        #region Get Paged
        public async Task<List<T>> GetPageAsync<TKey>(int PageNumber, int PageSize, Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> sortingExpression, SortDirection sortDir = SortDirection.Ascending, string includeProperties = "")
        {
            IQueryable<T> query = context.Set<T>();
            int skipCount = (PageNumber - 1) * PageSize;

            if (filter != null)
                query = query.Where(filter);

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            switch (sortDir)
            {
                case SortDirection.Ascending:
                    if (skipCount == 0)
                        query = query.OrderBy<T, TKey>(sortingExpression).Take(PageSize);
                    else
                        query = query.OrderBy<T, TKey>(sortingExpression).Skip(skipCount).Take(PageSize);
                    break;
                case SortDirection.Descending:
                    if (skipCount == 0)
                        query = query.OrderByDescending<T, TKey>(sortingExpression).Take(PageSize);
                    else
                        query = query.OrderByDescending<T, TKey>(sortingExpression).Skip(skipCount).Take(PageSize);
                    break;
                default:
                    break;
            }
            return await query.AsNoTracking().ToListAsync();
        }
        #endregion

        #region Get Individuals
        public async Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null)
        {
            return await entities.CountAsync(filter);
        }
        public async Task<bool> GetAnyAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = entities;
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.AsNoTracking().AnyAsync(filter);
        }
        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = context.Set<T>();
            if (filter != null)
                query = query.Where(filter).AsNoTracking();

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            T record = await query.FirstOrDefaultAsync();

            if (record != default)
                context.Entry(record).State = EntityState.Detached;

            return record;
        }

        public async Task<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = context.Set<T>();
            if (filter != null)
                query = query.Where(filter).AsNoTracking();
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            T record = await query.OrderByDescending(item => item).FirstOrDefaultAsync();

            if (record != default)
                context.Entry(record).State = EntityState.Detached;

            return record;
        }

      
        #endregion

        #endregion
    }

}
