using Application1.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application1.Contracts
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
