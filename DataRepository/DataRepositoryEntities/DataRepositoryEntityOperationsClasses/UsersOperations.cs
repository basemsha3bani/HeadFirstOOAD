using DataModel;
using DataRepository.DataRepositoryEntities.DataRepositoryOperationsInterface;
using DataRepository.DataRepositoryEntities.Security;
using DataRepository.GateWay;
using DataRepository.ModelMapper.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Enums;
using Utils.Enums.Classes;

namespace DataRepository.DataRepositoryEntities.DataRepositoryEntityOperationsClasses
{
    public class UsersOperations : IUsersOperations, IModelMapper<UsersDataModel>
    {

        private EnumMapper _enumMapper;
        public UsersOperations(EnumMapper enumMapper)
        {
            _enumMapper = enumMapper;
            ContextGateway<Users>.GetContextInstance();
        }
      

    

      

     

        public async Task<List<UsersDataModel>> CheckPasswordAsync(UsersDataModel SearchCriteria=null)
        {
            if (SearchCriteria == null)
            {
                List<UsersDataModel> _listOfUsers = (await ContextGateway<Users>.List()).Select
                     (Users => new UsersDataModel 
                     {
                            UserName = Users.UserName, 
                            Password = Users.Password, 
                            Role = Users.Role, 
                           IsAuthenticated=true,Message=""
                     }).ToList();
                return _listOfUsers;
            }
            
                List<UsersDataModel> listOfUsers = (from Users in
                                                      await ContextGateway<Users>.List(
                                                                 x =>   (x.UserName.ToLower() == SearchCriteria.UserName.ToLower())
                                                                        && (x.Password.ToLower() == SearchCriteria.Password.ToLower())
                                                              
                                                                                    )




                                           select      new UsersDataModel
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

        public UsersDataModel Map(IRepository RepoistoryObject)
        {
            Users Users = (Users)RepoistoryObject;

            return new UsersDataModel
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
