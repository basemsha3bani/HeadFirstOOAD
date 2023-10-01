using ViewModel;
using DataRepository;

using DataRepository.GateWay;
using DataRepository.ModelMapper.Interface;
using Domain.Entities.Operations.Interfaces;
using Domain.Entities.Schema.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Enums;
using Utils.Enums.Classes;
using ViewModel;
using Domain.Entities.Operations.Base;

namespace  Domain.Entities.Operations.Implemenation
{
    public class UsersOperations : BaseDomainOperations, IUsersOperations, IModelMapper<UsersViewModel>
    {

        private EnumMapper _enumMapper;
        public UsersOperations(EnumMapper enumMapper)
        {
            _enumMapper = enumMapper;
            ContextGateway<Users>.SetContextInstance(conext);

        }
      

    

      

     


        public async Task<List<UsersViewModel>> CheckPasswordAsync(UsersViewModel SearchCriteria=null)
        {
            if (SearchCriteria == null)
            {
                List<UsersViewModel> _listOfUsers = (await ContextGateway<Users>.List()).Select
                     (Users => new UsersViewModel 
                     {
                            UserName = Users.UserName, 
                            Password = Users.Password, 
                            Role = Users.Role, 
                           IsAuthenticated=true,Message=""
                     }).ToList();
                return _listOfUsers;
            }
            
                List<UsersViewModel> listOfUsers = (from Users in
                                                      await ContextGateway<Users>.List(
                                                                 x =>   (x.UserName.ToLower() == SearchCriteria.UserName.ToLower())
                                                                        && (x.Password.ToLower() == SearchCriteria.Password.ToLower())
                                                              
                                                                                    )




                                           select      new UsersViewModel
                     {
                                               UserName = Users.UserName,
                                               Password = Users.Password,
                                               Role = Users.Role,
                                               IsAuthenticated = true,
                                               Message = ""
                                           }).ToList();
            return listOfUsers;
           
           // return null;

        }

        public UsersViewModel Map(IRepository RepoistoryObject)
        {
            Users Users = (Users)RepoistoryObject;

            return new UsersViewModel
            {
                UserName = Users.UserName,
                Password = Users.Password,
                Role = Users.Role,
                IsAuthenticated = true,
                Message = ""

            };


        }

        
    }
    
}
