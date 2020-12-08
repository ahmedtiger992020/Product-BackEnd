using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

namespace Sample.Core.Entities
{
    public class Product :BaseEntity
    {

        #region Ctor
      
        #endregion

        #region Properties

        public string Name { get; set; }
        public string Photo { get; set; }
        public double Price { get; set; }

     

        #endregion

    }

}
