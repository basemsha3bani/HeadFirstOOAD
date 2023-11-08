using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Domain.Entities.Operations.Interfaces
{
    public interface IGuitarOperations
    {
        Task Add(GuitarViewModel Guitar);
        Task Edit(GuitarViewModel Guitar);



        Task Delete(int id);


        Task<GuitarViewModel> GetById(int id);

        Task<List<GuitarViewModel>> list(GuitarViewModel SearchCriteria = null);
    }
}
