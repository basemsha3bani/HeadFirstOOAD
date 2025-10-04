

using Application.EntityOperationsInterface;
using Application1.Contracts;
using Application1.Mapping.ModelMappingInterface;
using Application1.ViewModels;
using DataRepository.GateWay;


using Domain.Entities;
using Domain.Entities.Schema.Security;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Enums.Classes;

namespace Domain.DataRepositoryEntities.DataRepositoryEntityOperationsClasses
{
    public class UsersOperations : IUsersOperations, IViewModelMapper<Users>
    {

        private EnumMapper _enumMapper;
        IContextGateway<Users> _Usersepositry;
        public UsersOperations(EnumMapper enumMapper, IContextGateway<Users> UsersRepositry)
        {
            _enumMapper = enumMapper;
            _Usersepositry = UsersRepositry;
        }








        public async Task<UsersViewModel> CheckPasswordAsync(UsersViewModel SearchCriteria = null)
        {
           

            Users Users = (from user in
                                                  await _Usersepositry.List(
                                                             x => (x.UserName.ToLower() == SearchCriteria.UserName.ToLower())
                                                                    && (x.Password.ToLower() == SearchCriteria.Password.ToLower())

                                                                                )




                                                select user
                                                 
                                                ).FirstOrDefault();
            return (UsersViewModel) this.Map(Users);

            // return null;

        }



        public GenericViewModel Map(Users RepoistoryObject)
        {
            if(RepoistoryObject==null)
            {
                return null;
            }
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
