using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Utils.Enums.Classes
{
  
    public class EnumMapper
    {
        
        List<string> GuitarTypes { get; }
        List<string> BuilerTypes { get; }

        List<string> WoodTypes { get; }
        public string valueToEnum(string value,Type EnumType)
        {
            try
            {
                List<string> names =this.EnumTypeValues(EnumType);



               return names.Find(f => f.ToLower() == value.ToLower());
            }
            catch
            {
                return "";
            }

           

             

        } 

        private List<string> EnumTypeValues(Type type)
        {
            switch (type.Name)
            {
                case ("Builder"):
                    return BuilerTypes;
                   
                case ("GuitarType"):
                    return GuitarTypes;
                case ("Wood"):
                    return WoodTypes;

                default:
                    return new List<string>();
            }
        }

     public EnumMapper()
        {
            WoodTypes = new List<string>();
            WoodTypes.AddRange((string[])Enum.GetNames(typeof(Wood)));
            GuitarTypes = new List<string>();
            GuitarTypes.AddRange((string[])Enum.GetNames(typeof(GuitarType)));
            BuilerTypes = new List<string>();
            BuilerTypes.AddRange((string[])Enum.GetNames(typeof(Builder)));
          
        }
    }
}
