using Application1.Contracts;
using Application1.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application1.Features.Guitars.Commands.Handlers
{
    public class DeleteGuitarCommandHandler : IRequestHandler<GuitarViewModel>
    {
        IGuitarOperations _guitarOperations;


        public DeleteGuitarCommandHandler(IGuitarOperations guitarOperations)
        {
            _guitarOperations = guitarOperations;
        }

        public async Task<Unit> Handle(GuitarViewModel request, CancellationToken cancellationToken)
        {

            await _guitarOperations.Delete(int.Parse(request.serialNumber));
            return Unit.Value;

        }
    }
}
