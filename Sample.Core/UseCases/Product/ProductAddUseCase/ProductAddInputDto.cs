using System.Collections.Generic;

namespace Sample.Core.UseCases
{
    public class ProductAddInputDto
    {
        #region Props
        public string Name { get; set; }
        public string Photo { get; set; }
        public double Price { get; set; } 
        #endregion
    }
}