using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Core.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
