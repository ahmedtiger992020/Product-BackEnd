using Sample.SharedKernal;
using System;

namespace Sample.Core.UseCases
{
    public class ProductGetAllInputDto 
    {
        #region Properties
        public PagingModel Paging { get; set; }
        public SortingModel SortingModel { get; set; }
        #endregion
        public string Name { get; set; }
        public double Price { get; set; }
        //public DateTime? LastUpdated { get; set; }
    }
}