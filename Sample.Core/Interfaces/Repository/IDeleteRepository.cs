using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Sample.Core.Interfaces.Repository
{
    public interface IDeleteRepository<T> where T : class
    {
        #region Delete
        void HardDelete(T entity);
        #endregion
    }
}
