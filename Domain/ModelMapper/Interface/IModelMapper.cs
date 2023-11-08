using Domain.Entities;
using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataRepository.ModelMapper.Interface
{
    public interface IModelMapper<T> where T:Model
    {
        GenericViewModel Map(T model);
    }
}
