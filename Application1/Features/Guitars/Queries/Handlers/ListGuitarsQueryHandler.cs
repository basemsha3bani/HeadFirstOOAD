using Application1.Contracts;
using Application1.Mapping.ModelMappingInterface;
using Application1.ViewModels;
using Domain.Entities.Schema.dbo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application1.Features.Guitars.Queries.Handlers
{
    internal class ListGuitarsQueryHandler:IRequestHandler<ListGuitarsQuery, List<GuitarViewModel>>
    {
        private readonly IGuitarOperations _guitarOperations;
       

    public ListGuitarsQueryHandler(IGuitarOperations guitarOperations)
    {
        _guitarOperations = guitarOperations ?? throw new ArgumentNullException(nameof(guitarOperations));
        
    }

    
    
public async Task<List<GuitarViewModel>> Handle(ListGuitarsQuery request, CancellationToken cancellationToken)
        {
            var orderList = await _guitarOperations.list(request.searchCriteria);
            return (orderList);
        }
    }
}
