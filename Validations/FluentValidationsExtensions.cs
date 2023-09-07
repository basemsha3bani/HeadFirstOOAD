using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Utils.Enums.Classes;

namespace Validations
{
    internal static class FluentValidations
    {
        public static IRuleBuilderOptions<T, string> StringValueMustBeEnum<T>(this IRuleBuilder<T, string> ruleBuilder,Enum saeed)
        {
            EnumMapper enumMapper = new EnumMapper();
            return ruleBuilder.Must(val=> enumMapper.valueToEnum(val, saeed.GetType())!="");
        }
    }
}
