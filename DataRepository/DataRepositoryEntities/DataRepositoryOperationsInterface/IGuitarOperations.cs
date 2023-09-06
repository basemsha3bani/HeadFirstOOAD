using DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataRepositoryEntities.DataRepositoryOperationsInterface
{
   public interface IGuitarOperations
    {
        Task Add(GuitarDataModel Guitar);
        Task Edit(GuitarDataModel Guitar);



        Task Delete(int id);


        Task< GuitarDataModel> GetById(int id);

       Task<List<GuitarDataModel>> list(GuitarDataModel SearchCriteria=null);
    }
}
