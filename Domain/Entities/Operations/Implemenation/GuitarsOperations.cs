using ViewModel;
<<<<<<<< HEAD:Application/EntityOperationsClasses/GuitarsOperations.cs

using Domain.ViewModel;
========
using DataRepository;

using DataRepository.GateWay;
using DataRepository.ModelMapper.Interface;
using Domain.Entities.Operations.Interfaces;
>>>>>>>> 374eb4af2575c8659ad3d8e8e4de3e6f4efa6188:Domain/Entities/Operations/Implemenation/GuitarsOperations.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.DomainEntities;
using DataRepository.GateWay;
using Utils.Enums;
using Utils.Enums.Classes;
<<<<<<<< HEAD:Application/EntityOperationsClasses/GuitarsOperations.cs
using DataRepository.ModelMapper.Interface;
using Domain.Entities;
using Application.EntityOperationsInterface;

namespace Domain.DataRepositoryEntities.DataRepositoryEntityOperationsClasses
{
    public class GuitarOperations : IGuitarOperations, IModelMapper<Guitar>
========
using Domain.Entities.Schema.dbo;
using Domain.Entities.Operations.Base;

namespace  Domain.Entities.Operations.Implemenation
{
    public class GuitarOperations : BaseDomainOperations,IGuitarOperations, IModelMapper<GuitarViewModel>
>>>>>>>> 374eb4af2575c8659ad3d8e8e4de3e6f4efa6188:Domain/Entities/Operations/Implemenation/GuitarsOperations.cs
    {

        private EnumMapper _enumMapper;
        IContextGateway<Guitar> _GuitarRepositry;
        public GuitarOperations(EnumMapper enumMapper, IContextGateway<Guitar> GuitarRepositry)
        {
            ContextGateway<Guitar>.SetContextInstance(conext);
            _enumMapper = enumMapper;
<<<<<<<< HEAD:Application/EntityOperationsClasses/GuitarsOperations.cs
            _GuitarRepositry = GuitarRepositry;
         
        }

       

========
            
        }
>>>>>>>> 374eb4af2575c8659ad3d8e8e4de3e6f4efa6188:Domain/Entities/Operations/Implemenation/GuitarsOperations.cs
        public async Task Add(GuitarViewModel Guitar)
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


            await _GuitarRepositry.Add(GuitarInstance);


        }



        public async Task Delete(int id)
        {
            Guitar GuitarInstance = new Guitar();
            GuitarInstance = await _GuitarRepositry.GetById(g => g.serialNumber == id.ToString());
            await _GuitarRepositry.Delete(GuitarInstance);
        }

        public async Task Edit(GuitarViewModel Guitar)
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
            Guitar GuitarOld = await _GuitarRepositry.GetById(g => g.serialNumber == guitarNew.serialNumber);
            await _GuitarRepositry.Edit(GuitarOld, guitarNew);
        }

<<<<<<<< HEAD:Application/EntityOperationsClasses/GuitarsOperations.cs


========
>>>>>>>> 374eb4af2575c8659ad3d8e8e4de3e6f4efa6188:Domain/Entities/Operations/Implemenation/GuitarsOperations.cs
        public async Task<GuitarViewModel> GetById(int id)
        {
            Guitar GuitarInstance = new Guitar();
            GuitarInstance = new Guitar();
<<<<<<<< HEAD:Application/EntityOperationsClasses/GuitarsOperations.cs
            GuitarInstance = await _GuitarRepositry.GetById(g => g.serialNumber == id.ToString());
========
            GuitarInstance = await ContextGateway<Guitar>.GetById(g => g.serialNumber == id.ToString());
>>>>>>>> 374eb4af2575c8659ad3d8e8e4de3e6f4efa6188:Domain/Entities/Operations/Implemenation/GuitarsOperations.cs
            return (GuitarViewModel)this.Map(GuitarInstance);

            
        }

        public async Task<List<GuitarViewModel>> list(GuitarViewModel SearchCriteria=null)
        {
            if (SearchCriteria == null)
            {
<<<<<<<< HEAD:Application/EntityOperationsClasses/GuitarsOperations.cs
                List<GuitarViewModel> _listOfGuitar = (await _GuitarRepositry.List()).Select
========
                List<GuitarViewModel> _listOfGuitar = (await ContextGateway<Guitar>.List()).Select
>>>>>>>> 374eb4af2575c8659ad3d8e8e4de3e6f4efa6188:Domain/Entities/Operations/Implemenation/GuitarsOperations.cs
                     (guitar => new GuitarViewModel 
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
            
                List<GuitarViewModel> listOfGuitar = (from guitar in
<<<<<<<< HEAD:Application/EntityOperationsClasses/GuitarsOperations.cs
                                                      await _GuitarRepositry.List(
========
                                                      await ContextGateway<Guitar>.List(
>>>>>>>> 374eb4af2575c8659ad3d8e8e4de3e6f4efa6188:Domain/Entities/Operations/Implemenation/GuitarsOperations.cs
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




                                           select      new GuitarViewModel
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

<<<<<<<< HEAD:Application/EntityOperationsClasses/GuitarsOperations.cs
       

        

        public GenericViewModel Map(Guitar guitar)
        {
========
        public GuitarViewModel Map(IRepository RepoistoryObject)
        {
            Guitar guitar = (Guitar)RepoistoryObject;

>>>>>>>> 374eb4af2575c8659ad3d8e8e4de3e6f4efa6188:Domain/Entities/Operations/Implemenation/GuitarsOperations.cs
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
    }
    
}
