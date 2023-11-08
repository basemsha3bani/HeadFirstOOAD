using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    public interface IGuitarServices
    {
        public Task Add(GuitarViewModel Guitar);

        public Task Delete(int id);


        public Task Edit(GuitarViewModel model);

        public Task<GuitarViewModel> GetById(int id);
        public Task<List<GuitarViewModel>> list(GuitarViewModel SearchCriteria = null);
    }
    
}
