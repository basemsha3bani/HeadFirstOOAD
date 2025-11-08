

using Application.EntityOperationsInterface;
using Application.Features.Users;
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
    public class UsersOperations : IUsersOperations
    {

        private EnumMapper _enumMapper;
       
        UserManager<ApplicationUser> _userManager;
        AuthService _authService;
        RegistrationService _registrationService;
        public UsersOperations(EnumMapper enumMapper, UserManager<ApplicationUser> userManager,AuthService authService, RegistrationService  registrationService)
        {
            _enumMapper = enumMapper;
            
            _userManager = userManager;
            _authService =authService ;
            _registrationService = registrationService;
        }








        public async Task<UsersViewModel> CheckPasswordAsync(UsersViewModel SearchCriteria = null)
                    {


            
                return await _authService.GetTokenAsync(SearchCriteria);

            

        }

        public async Task<UsersViewModel> CreateUserAsync(UsersViewModel SearchCriteria = null)
        {
            return await _registrationService.RegisterAsync (SearchCriteria);  
        }
    }
}
