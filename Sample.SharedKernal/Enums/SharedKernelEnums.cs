using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.SharedKernal.Enums
{
    public class SharedKernelEnums
    {
        public enum SortDirection
        {
            Ascending = 0,
            Descending = 1
        }
        public enum Paging
        {
            DefaultPageNumber = 1,
            DefaultPageSize = 10
        }

        //public enum StatusEnum
        //{
        //    NotChanged = 1,
        //    New = 2,
        //    Updated = 3,
        //    Deleted = 4
        //}
    }
}
