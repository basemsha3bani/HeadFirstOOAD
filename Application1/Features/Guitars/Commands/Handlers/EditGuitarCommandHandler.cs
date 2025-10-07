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

namespace Application1.Features.Guitars.Commands.Handlers
{
    internal class EditGuitarCommandHandler : IRequestHandler<GuitarViewModel>
    {
        IGuitarOperations _guitarOperations;
        

        public EditGuitarCommandHandler(IGuitarOperations guitarOperations)
        {
            _guitarOperations = guitarOperations;
           
        }
    
        public async Task<Unit> Handle(GuitarViewModel request, CancellationToken cancellationToken)
        {
           
           await _guitarOperations.Add(request);
            return Unit.Value;
        }
    }
}
