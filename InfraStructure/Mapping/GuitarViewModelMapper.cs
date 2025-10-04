using Application1.Mapping.ModelMappingInterface;
using Application1.ViewModels;
using Domain.Entities.Schema.dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Enums.Classes;
using Utils.Enums;
using Domain.Entities;

namespace InfraStructure.Mapping
{
    internal class GuitarViewModelMapper : IModelMapper<GuitarViewModel>
    {
        private EnumMapper _enumMapper;

        public GuitarViewModelMapper(EnumMapper enumMapper)
        {
            _enumMapper = enumMapper;
        }

        

        Model IModelMapper<GuitarViewModel>.Map(GuitarViewModel guitarViewModel)
        {
            Guitar guitarNew = new Guitar
            {
                backWood = _enumMapper.valueToEnum(guitarViewModel.backWood, typeof(Wood)),
                builder = _enumMapper.valueToEnum(guitarViewModel.builder, typeof(Builder)),
                model = guitarViewModel.model,
                price = guitarViewModel.price,
                serialNumber = guitarViewModel.serialNumber,
                topWood = _enumMapper.valueToEnum(guitarViewModel.topWood, typeof(Wood)),
                type = _enumMapper.valueToEnum(guitarViewModel.type, typeof(GuitarType)),
            };
            return guitarNew;
        }
    }
}
