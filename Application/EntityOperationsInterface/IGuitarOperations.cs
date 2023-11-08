using ViewModel;
using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.EntityOperationsInterface
{
    public interface IGuitarOperations
    {
        Task Add(GuitarViewModel Guitar);
        Task Edit(GuitarViewModel Guitar);



        Task Delete(int id);


        Task< GuitarViewModel> GetById(int id);

       Task<List<GuitarViewModel>> list(GuitarViewModel SearchCriteria=null);
    }
}
