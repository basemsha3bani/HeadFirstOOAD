using DataModel;
using DataRepository.DataRepositoryEntities.DataRepositoryOperationsInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Validations;

namespace ServicesClasses
{
    public class GuitarService
    {
        private readonly IGuitarOperations _GuitarOperations;
        private readonly GuitarValidator _GuitarValidator;

        public GuitarService(IGuitarOperations GuitarOperations, GuitarValidator GuitarValidator)
    {
        _GuitarOperations = GuitarOperations;
        _GuitarValidator = GuitarValidator;
    }
    public async Task Add(GuitarDataModel Guitar)
    {
            var validationResult = _GuitarValidator.Validate(Guitar);
            if(!validationResult.IsValid)
            {
                throw new Exception(validationResult.Errors[0].ToString());
            }
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
