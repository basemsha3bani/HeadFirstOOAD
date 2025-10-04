using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application1.ViewModels
{
    
        public class GuitarViewModel  :GenericViewModel,IRequest
        {
            #region Fields


            #endregion

            #region Properties
            public string serialNumber { get; set; } = "";
            public double price { get; set; } = 0;
            public string builder { get; set; } = "";
            public string model { get; set; } = "";
            public string type { get; set; } = "";
            public string backWood { get; set; } = "";
            public string topWood { get; set; } = "";

            #endregion

            #region Initialization

            #endregion


        }
    }

