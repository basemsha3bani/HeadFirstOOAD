
using Application1.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.EntityOperationsInterface
{
    public interface IUsersOperations
    {
        Task<UsersViewModel> CheckPasswordAsync(UsersViewModel SearchCriteria = null);
    }
}