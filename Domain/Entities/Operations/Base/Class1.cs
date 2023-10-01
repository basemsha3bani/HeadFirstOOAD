using DataRepository.GateWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Operations.Base
{
    public class BaseDomainOperations
    {
        protected static DbConext conext;
        
        protected BaseDomainOperations()
        {
            if (conext == null)
            {
                conext = new DbConext();
               
            }
        }
    }
   

    }
