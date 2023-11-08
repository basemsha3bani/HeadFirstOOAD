
using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServicesClasses.Interfaces
{
    public interface IAuthService
    {
        Task<UsersViewModel> GetTokenAsync(UsersViewModel model);
    }
}
