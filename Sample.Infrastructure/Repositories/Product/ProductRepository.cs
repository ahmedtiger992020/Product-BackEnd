using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Sample.Core.Interfaces.Repository;
using Sample.Infrastructure.Context;
using Sample.Core.Entities;

namespace Sample.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        DbSet<Product> entity;
        #region CTRS
        public ProductRepository(SampleContext context) : base(context)
        {
            entity = context.Set<Product>();
        }
        #endregion
    }
}
