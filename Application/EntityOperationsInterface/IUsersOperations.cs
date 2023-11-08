using Domain.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.EntityOperationsInterface
{
    public interface IUsersOperations
    {
        Task<List<UsersViewModel>> CheckPasswordAsync(UsersViewModel SearchCriteria = null);
    }
}