using DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServicesClasses.Interfaces
{
    public interface IAuthService
    {
        Task<LoginDataModel> GetTokenAsync(LoginDataModel model);
    }
}
