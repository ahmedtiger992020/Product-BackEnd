using System;
using System.Collections.Generic;
using Sample.SharedKernal;

namespace Sample.Core.UseCases
{
    public class ProductGetAllOutputDto : IdNameDto
    {
        #region Properties
        public string Photo { get; set; }
        public double Price { get; set; }
        public DateTime? LastUpdated { get; set; }

        #endregion

    }
}