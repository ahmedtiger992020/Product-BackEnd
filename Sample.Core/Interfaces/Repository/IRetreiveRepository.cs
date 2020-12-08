using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sample.SharedKernal.Enums;
using static Sample.SharedKernal.Enums.SharedKernelEnums;

namespace Sample.Core.Interfaces.Repository
{
    public interface IRetreiveRepository<T> where T : class 
    {
        #region Retreive

        #region GetById
        Task<T> GetById(int Id);
        #endregion

        #region GetList
        Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        Task<List<T>> GetWhereAsync<TKey>(Expression<Func<T, bool>> filter = null, string includeProperties = "", Expression<Func<T, TKey>> sortingExpression = null, SortDirection sortDir = SortDirection.Ascending);
        #endregion

        #region Get Paged
        Task<List<T>> GetPageAsync<TKey>(int PageNumeber, int PageSize, Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> sortingExpression, SharedKernelEnums.SortDirection sortDir = SharedKernelEnums.SortDirection.Ascending, string includeProperties = "");
        #endregion

        #region Get Individuals
        Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null);
        Task<bool> GetAnyAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        Task<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> filter = null,string includeProperties = "");
     
        #endregion

        #endregion
    }
}
