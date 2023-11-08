
using Application.EntityOperationsInterface;
using DataRepository.GateWay;
using DataRepository.ModelMapper.Interface;

using Domain.Entities;
using Domain.Entities.Schema.Security;
using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Enums.Classes;

namespace Domain.DataRepositoryEntities.DataRepositoryEntityOperationsClasses
{
    public class UsersOperations : IUsersOperations, IModelMapper<Users>
    {

        private EnumMapper _enumMapper;
        IContextGateway<Users> _Usersepositry;
        public UsersOperations(EnumMapper enumMapper, IContextGateway<Users> UsersRepositry)
        {
            _enumMapper = enumMapper;
            _Usersepositry = UsersRepositry;
        }








        public async Task<List<UsersViewModel>> CheckPasswordAsync(UsersViewModel SearchCriteria = null)
        {
            if (SearchCriteria == null)
            {
                List<UsersViewModel> _listOfUsers = (from Users in
                                                      await _Usersepositry.List()




                                                     select new UsersViewModel
                                                     {
                                                         UserName = Users.UserName,
                                                         Password = Users.Password,
                                                         Role = Users.Role,
                                                         IsAuthenticated = true,
                                                         Message = ""
                                                     }).ToList();
                return _listOfUsers;
            }

            List<UsersViewModel> listOfUsers = (from Users in
                                                  await _Usersepositry.List(
                                                             x => (x.UserName.ToLower() == SearchCriteria.UserName.ToLower())
                                                                    && (x.Password.ToLower() == SearchCriteria.Password.ToLower())

                                                                                )




                                                select new UsersViewModel
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



        public GenericViewModel Map(Users RepoistoryObject)
        {

            Users user = (Users)RepoistoryObject;

            return new UsersViewModel
            {
                UserName = user.UserName,
                Password = user.Password,
                Role = user.Role,
                IsAuthenticated = true,
                Message = ""

            };

        }


    }
}
