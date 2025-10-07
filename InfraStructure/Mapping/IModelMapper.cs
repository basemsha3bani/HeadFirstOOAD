using Application1.ViewModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Mapping
{
    public interface IModelMapper<T> where T : GenericViewModel
    {
        Model MapToModel(T model);
    }
}
