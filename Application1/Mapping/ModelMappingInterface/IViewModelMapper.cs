using Application1.ViewModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application1.Mapping.ModelMappingInterface
{
    public interface IViewModelMapper<T> where T : Model
    {
        GenericViewModel Map(T model);
    }
}
