using ViewModel;
using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.EntityOperationsInterface
{
    public interface IUsersOperations
    {
        Task<List<UsersViewModel>> CheckPasswordAsync(UsersViewModel SearchCriteria = null);
    }
}
