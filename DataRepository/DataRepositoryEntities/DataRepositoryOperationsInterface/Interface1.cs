using DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataRepositoryEntities.DataRepositoryOperationsInterface
{
    public interface IUsersOperations
    {
        Task<List<UsersDataModel>> CheckPasswordAsync(UsersDataModel SearchCriteria = null);
    }
}
