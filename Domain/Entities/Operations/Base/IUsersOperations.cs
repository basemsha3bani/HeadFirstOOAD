using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Domain.Entities.Operations.Interfaces
{
    public interface IUsersOperations
    {
        Task<List<UsersViewModel>> CheckPasswordAsync(UsersViewModel SearchCriteria = null);
    }
}
