using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DomainEntities
{
    public class Guitar:Model
    {
        #region Fields


        #endregion

        #region Properties
        public string serialNumber { get; set; }
        public double price { get; set; }
        public string builder { get; set; }
        public string model { get; set; }
        public string type { get; set; }
        public string backWood { get; set; }
        public string topWood { get; set; }

        #endregion

        #region Initialization

        #endregion
    }
}
