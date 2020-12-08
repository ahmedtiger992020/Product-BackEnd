using System.Collections.Generic;
using Sample.SharedKernal;

namespace Sample.Core.UseCases
{
    public class ProductUpdateInputDto : IdNameDto
    {
        public string Photo { get; set; }
        public double Price { get; set; }

    }
}