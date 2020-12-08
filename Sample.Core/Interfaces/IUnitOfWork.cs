using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Core.Interfaces
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Commit changes
        /// </summary>
        Task<int> Commit();

        /// <summary>
        /// Commit changes and throw error if commit failed
        /// </summary>
        /// <param name="_culture">Culture to throw the exception message with.</param>
        Task<int> Commit(string _culture);
    }
}
