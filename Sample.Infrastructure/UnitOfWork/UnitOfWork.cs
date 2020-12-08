using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sample.Core.Interfaces;
using Sample.Infrastructure.Context;
using Sample.SharedKernal.Localization;

namespace Sample.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Defines the Context
        /// </summary>
        public SampleContext _context;

        private readonly ILocalizationReader _messageResource;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="SampleContext"/></param>
        public UnitOfWork(SampleContext context/*, ILocalizationReader messageResource*/)
        {
            _context = context;
            //_messageResource = messageResource;
        }

        /// <summary>
        /// Commit changes
        /// </summary>
        public async Task<int> Commit()
        {
            try
            {
                int status = await _context.SaveChangesAsync();

                return status;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Commit changes and throw error if commit failed
        /// </summary>
        /// <param name="_culture">Culture to throw the exception message with.</param
        public async Task<int> Commit(string _culture)
        {
            try
            {
                int status = await _context.SaveChangesAsync();
                if (status <= default(int))
                    throw new Exception(_messageResource.CommitFailed(_culture));
                return status;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
