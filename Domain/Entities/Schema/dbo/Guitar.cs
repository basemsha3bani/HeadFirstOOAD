<<<<<<<< HEAD:Domain/Entities/Guitar.cs
﻿using Domain.Entities;
========
﻿using DataRepository;
>>>>>>>> 374eb4af2575c8659ad3d8e8e4de3e6f4efa6188:Domain/Entities/Schema/dbo/Guitar.cs
using System;
using System.Collections.Generic;
using System.Text;

<<<<<<<< HEAD:Domain/Entities/Guitar.cs
namespace Domain.DomainEntities
{
    public class Guitar:Model
========
namespace Domain.Entities.Schema.dbo
{
   public class Guitar : IRepository
>>>>>>>> 374eb4af2575c8659ad3d8e8e4de3e6f4efa6188:Domain/Entities/Schema/dbo/Guitar.cs
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
