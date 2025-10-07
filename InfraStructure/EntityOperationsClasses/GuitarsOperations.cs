
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application1.Contracts;
using Application1.Mapping.ModelMappingInterface;
using Application1.ViewModels;
using Azure.Core;
using DataRepository.GateWay;

using Domain.Entities;
using Domain.Entities.Schema.dbo;
using InfraStructure.Mapping;
using Utils.Enums;
using Utils.Enums.Classes;

namespace Domain.DataRepositoryEntities.DataRepositoryEntityOperationsClasses
{
    public class GuitarOperations : IGuitarOperations,IViewModelMapper<Guitar>,IModelMapper<GuitarViewModel>
    { 

        private EnumMapper _enumMapper;
        IContextGateway<Guitar> _GuitarRepositry;
        public GuitarOperations(EnumMapper enumMapper, IContextGateway<Guitar> GuitarRepositry)
        {
            _enumMapper = enumMapper;
            _GuitarRepositry = GuitarRepositry;

        }



        public async Task Add(GuitarViewModel Guitar)
        {
            Guitar GuitarInstance =(Guitar) this.MapToModel(Guitar);


            await _GuitarRepositry.Add(GuitarInstance);


        }

        
        

        public async Task Delete(int id)
        {
            Guitar GuitarInstance = new Guitar();
            GuitarInstance = await _GuitarRepositry.GetById(g => g.serialNumber == id.ToString());

            
            if (GuitarInstance != null)
            {
                await _GuitarRepositry.Delete(GuitarInstance);
            }
           
        }

        public async Task Edit(GuitarViewModel Guitar)
        {
            Guitar guitarNew = new Guitar
            {
                backWood = _enumMapper.valueToEnum(Guitar.backWood, typeof(Wood)),
                builder = _enumMapper.valueToEnum(Guitar.builder, typeof(Builder)),
                model = Guitar.model,
                price = Guitar.price,
                serialNumber = Guitar.serialNumber,
                topWood = _enumMapper.valueToEnum(Guitar.topWood, typeof(Wood)),
                type = _enumMapper.valueToEnum(Guitar.type, typeof(GuitarType)),
            };
            Guitar GuitarOld = await _GuitarRepositry.GetById(g => g.serialNumber == guitarNew.serialNumber);
            await _GuitarRepositry.Edit(GuitarOld, guitarNew);
        }

       

        public async Task<GuitarViewModel> GetById(int id)
        {
            Guitar GuitarInstance = new Guitar();
            GuitarInstance = new Guitar();
            GuitarInstance = await _GuitarRepositry.GetById(g => g.serialNumber == id.ToString());
            return (GuitarViewModel)this.MapToViewModel(GuitarInstance);


        }

        public async Task<List<GuitarViewModel>> list(GuitarViewModel SearchCriteria = null)
        {
            if (SearchCriteria == null)
            {
                List<GuitarViewModel> _listOfGuitar = (await _GuitarRepositry.List()).Select
                     (guitar => new GuitarViewModel
                     {
                         backWood = guitar.backWood,
                         builder = guitar.builder,
                         model = guitar.model,
                         price = guitar.price,
                         serialNumber = guitar.serialNumber,
                         topWood = guitar.topWood,
                         type = guitar.type
                     }).ToList();
                return _listOfGuitar;
            }

            List<GuitarViewModel> listOfGuitar = (from guitar in
                                                  await _GuitarRepositry.List(
                                                             x =>
                                                             (x.backWood == _enumMapper.valueToEnum(SearchCriteria.backWood, typeof(Wood))
                                                             || SearchCriteria.backWood == "")
                                                             && (x.builder == _enumMapper.valueToEnum(SearchCriteria.builder, typeof(Builder))
                                                             || SearchCriteria.builder == "")
                                                             && (x.model == SearchCriteria.model
                                                             || SearchCriteria.model == "")
                                                             && (x.price == SearchCriteria.price
                                                             || SearchCriteria.price == 0)
                                                             && (x.serialNumber == SearchCriteria.serialNumber
                                                             || SearchCriteria.serialNumber == "")
                                                             && (x.topWood == _enumMapper.valueToEnum(SearchCriteria.topWood, typeof(Wood))
                                                             || SearchCriteria.topWood == "")
                                                              && (x.type == _enumMapper.valueToEnum(SearchCriteria.type, typeof(GuitarType))
                                                              || SearchCriteria.type == "")
                                                              )




                                                  select new GuitarViewModel
                                                  {
                                                      backWood = guitar.backWood,
                                                      builder = guitar.builder,
                                                      model = guitar.model,
                                                      price = guitar.price,
                                                      serialNumber = guitar.serialNumber,
                                                      topWood = guitar.topWood,
                                                      type = guitar.type
                                                  }).ToList();
            return listOfGuitar;

            // return null;

        }

       

        public GenericViewModel MapToViewModel(Guitar guitar)
        {
            return new GuitarViewModel
            {
                backWood = _enumMapper.valueToEnum(guitar.backWood, typeof(Wood)),
                builder = _enumMapper.valueToEnum(guitar.builder, typeof(Builder)),
                model = guitar.model,
                price = guitar.price,
                serialNumber = guitar.serialNumber,
                topWood = _enumMapper.valueToEnum(guitar.topWood, typeof(Wood)),
                type = _enumMapper.valueToEnum(guitar.type, typeof(GuitarType)),

            };
        }

        public Model MapToModel(GuitarViewModel model)
        {
            return new Guitar
            {
                backWood = _enumMapper.valueToEnum(model.backWood, typeof(Wood)),
                builder = _enumMapper.valueToEnum(model.builder, typeof(Builder)),
                model = model.model,
                price = model.price,
                serialNumber = model.serialNumber,
                topWood = _enumMapper.valueToEnum(model.topWood, typeof(Wood)),
                type = _enumMapper.valueToEnum(model.type, typeof(GuitarType)),

            };
        }

        Task<GuitarViewModel> IGuitarOperations.GetById(int id)
        {
            throw new NotImplementedException();
        }

        
    }

}
