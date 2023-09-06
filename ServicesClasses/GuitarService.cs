using DataModel;
using DataRepository.DataRepositoryEntities.DataRepositoryOperationsInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServicesClasses
{
    public class GuitarService
    {
        private readonly IGuitarOperations _GuitarOperations;
     
    public GuitarService(IGuitarOperations GuitarOperations)
    {
        _GuitarOperations = GuitarOperations;

    }
    public async Task Add(GuitarDataModel Guitar)
    {
      await  _GuitarOperations.Add(Guitar);
    }

    public async Task Delete(int id)
    {
        _GuitarOperations.Delete(id);
    }

    public async Task Edit(GuitarDataModel model)
    {
      await  _GuitarOperations.Edit(model);
    }

    public async Task< GuitarDataModel> GetById(int id)
    {
        return await _GuitarOperations.GetById(id);

    }

    public async Task<List<GuitarDataModel>> list(GuitarDataModel SearchCriteria= null)
    {
        return await _GuitarOperations.list(SearchCriteria);
    }
}
}
