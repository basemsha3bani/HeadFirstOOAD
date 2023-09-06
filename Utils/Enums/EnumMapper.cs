using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Utils.Enums
{
    public class EnumMapper
    {
        public string valueToEnum(string value,Type EnumType)
        {
            try
            {
                List<string> names = new List<string>();
                names.AddRange((string[])Enum.GetNames(EnumType));



               return names.Find(f => f.ToLower() == value.ToLower());
            }
            catch
            {
                return "";
            }

           

             

        } 

        
    }
}
