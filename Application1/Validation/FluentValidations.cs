using Application1.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Enums;
using Utils.Enums.Classes;

namespace Application1.Validation
{
    public class GuitarValidator : AbstractValidator<GuitarViewModel>
    {
        private string Keyvalue = string.Empty;
        private EnumMapper _enumMapper;
        enum MyEnum
        {

        }
        public GuitarValidator(EnumMapper enumMapper)
        {
            //Keyvalue = keyvalue;
            //RuleFor(model => model.backWood).StringValueMustBeEnum(typeof(Wood));
            _enumMapper = enumMapper;
            RuleFor(model => model.type).Must(backWoodValue => StringValueMustBeEnum(backWoodValue, typeof(GuitarType))).WithMessage("Invalid guitar Type");
            RuleFor(model => model.topWood).Must(backWoodValue => StringValueMustBeEnum(backWoodValue, typeof(Wood))).WithMessage("Invalid topWood Type");
            RuleFor(model => model.builder).Must(backWoodValue => StringValueMustBeEnum(backWoodValue, typeof(Builder))).WithMessage("Invalid builder Type");
            RuleFor(model => model.backWood).Must(backWoodValue => StringValueMustBeEnum(backWoodValue, typeof(Wood))).WithMessage("Invalid BackWood Type");
        }

        private bool StringValueMustBeEnum(string value, Type EnumType)
        {

            return _enumMapper.valueToEnum(value, EnumType) != null;
        }
    }
}
