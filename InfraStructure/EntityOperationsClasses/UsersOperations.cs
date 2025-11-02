

using Application.EntityOperationsInterface;
using Application1.Contracts;
using Application1.Mapping.ModelMappingInterface;
using Application1.ViewModels;
using DataRepository.GateWay;


using Domain.Entities;
using Domain.Entities.Schema.Security;
using Microsoft.AspNetCore.Identity;
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
        UserManager<Users> _userManager;
        public UsersOperations(EnumMapper enumMapper, IContextGateway<Users> UsersRepositry,UserManager<Users> userManager)
        {
            _enumMapper = enumMapper;
            _Usersepositry = UsersRepositry;
            _userManager = userManager; 
        }








        public async Task<UsersViewModel> CheckPasswordAsync(UsersViewModel SearchCriteria = null)
        {
           

            Users User = await _userManager.FindByNameAsync(SearchCriteria.UserName);
            if (User != null && await _userManager.CheckPasswordAsync(User, SearchCriteria.Password))
            return (UsersViewModel) this.MapToViewModel(User);

             return null;

        }



        public GenericViewModel MapToViewModel(Users RepoistoryObject)
        {
            if(RepoistoryObject==null)
            {
                return null;
            }
            Users user = (Users)RepoistoryObject;

            return new UsersViewModel
            {
                UserName = user.UserName,
                
               
                IsAuthenticated = true,
                Message = ""

            };

        }


    }
}
