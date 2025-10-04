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
    internal class GetGuitarByIdHandler : IRequestHandler<GetGuitarByIdQuery, GuitarViewModel>
    {
        private readonly IGuitarOperations _guitarOperations;


        public GetGuitarByIdHandler(IGuitarOperations guitarOperations)
        {
            _guitarOperations = guitarOperations ?? throw new ArgumentNullException(nameof(guitarOperations));
        }



        public async Task<GuitarViewModel> Handle(GetGuitarByIdQuery request, CancellationToken cancellationToken)
        {
            var guitar = await _guitarOperations.GetById((int.Parse(request.searchCriteria.serialNumber)));
            return (guitar);
        }
    }
}

   

