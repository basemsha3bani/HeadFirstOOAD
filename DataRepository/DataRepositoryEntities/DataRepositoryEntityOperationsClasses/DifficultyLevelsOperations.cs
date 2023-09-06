using DataModel;
using DataRepository.DataRepositoryEntities.DataRepositoryOperationsInterface;
using DataRepository.GateWay;
using DataRepository.ModelMapper.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Enums;

namespace DataRepository.DataRepositoryEntities.DataRepositoryEntityOperationsClasses
{
    public class GuitarOperations : IGuitarOperations, IModelMapper<GuitarDataModel>
    {

        private EnumMapper _enumMapper;
        public GuitarOperations(EnumMapper enumMapper)
        {
            _enumMapper = enumMapper;
            ContextGateway<Guitar>.GetContextInstance();
        }
        public async Task Add(GuitarDataModel Guitar)
        {
            Guitar GuitarInstance = new Guitar {
                backWood = Guitar.backWood,
                builder = Guitar.builder,
                model = Guitar.model,
                price = Guitar.price,
                serialNumber = Guitar.serialNumber,
                topWood = Guitar.topWood,
                type = Guitar.type
            };


            await ContextGateway<Guitar>.Add(GuitarInstance);


        }

        public async Task Delete(int id)
        {
            Guitar GuitarInstance = new Guitar();
            GuitarInstance = await ContextGateway<Guitar>.GetById(g => g.serialNumber == id.ToString());
            await ContextGateway<Guitar>.Delete(GuitarInstance);
        }

        public async Task Edit(GuitarDataModel Guitar)
        {
            Guitar guitarNew= new Guitar
            {
                backWood = _enumMapper.valueToEnum( Guitar.backWood,typeof(Wood)),
                builder = _enumMapper.valueToEnum(Guitar.builder, typeof(Builder)) ,
                model = Guitar.model,
                price = Guitar.price,
                serialNumber = Guitar.serialNumber,
                topWood = _enumMapper.valueToEnum(Guitar.topWood, typeof(Wood)),
                type = _enumMapper.valueToEnum(Guitar.type, typeof(GuitarType)),
            };
            Guitar GuitarOld = await ContextGateway<Guitar>.GetById(g => g.serialNumber == guitarNew.serialNumber);
            await ContextGateway<Guitar>.Edit(GuitarOld, guitarNew);
        }

        public async Task<GuitarDataModel> GetById(int id)
        {
            Guitar GuitarInstance = new Guitar();
            GuitarInstance = new Guitar();
            GuitarInstance = await ContextGateway<Guitar>.GetById(g => g.serialNumber == id.ToString());
            return (GuitarDataModel)this.Map(GuitarInstance);

            
        }

        public async Task<List<GuitarDataModel>> list(GuitarDataModel SearchCriteria=null)
        {
            if (SearchCriteria == null)
            {
                List<GuitarDataModel> _listOfGuitar = (await ContextGateway<Guitar>.List()).Select
                     (guitar => new GuitarDataModel 
                     {
                            backWood = guitar.backWood, 
                            builder = guitar.builder, 
                            model = guitar.model, 
                            price = guitar.price,
                            serialNumber = guitar.serialNumber, 
                             topWood = guitar.topWood, 
                            type = guitar.type }).ToList();
                return _listOfGuitar;
            }
            
                List<GuitarDataModel> listOfGuitar = (from guitar in
                                                      await ContextGateway<Guitar>.List(
                                                                 x =>
                                                                 (x.backWood ==_enumMapper.valueToEnum( SearchCriteria.backWood,typeof(Wood))
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




                                           select      new GuitarDataModel
                     {
                backWood = guitar.backWood, 
                            builder = guitar.builder, 
                            model = guitar.model, 
                            price = guitar.price,
                            serialNumber = guitar.serialNumber, 
                             topWood = guitar.topWood, 
                            type = guitar.type }).ToList();
            return listOfGuitar;
           
           // return null;

        }

        public GuitarDataModel Map(IRepository RepoistoryObject)
        {
            Guitar guitar = (Guitar)RepoistoryObject;

            return new GuitarDataModel
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

       




    }
    
}
