using Application1.Contracts;
using Application1.Validation;
using Application1.ViewModels;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application1.Features.Guitars.Commands.Handlers
{
    
    internal class CreateGuitarCommandHandler : IRequestHandler<GuitarViewModel>
    {
        IGuitarOperations _guitarOperations;
        GuitarValidator _validations;


        public CreateGuitarCommandHandler(IGuitarOperations guitarOperations, GuitarValidator validations)
        {
            _guitarOperations = guitarOperations;
            _validations = validations;

        }

        public async Task<Unit> Handle(GuitarViewModel request, CancellationToken cancellationToken)
        {
           ValidationResult result= await _validations.ValidateAsync(request);
            if(!result.IsValid)
            {
                throw new Exception(result.Errors.ElementAt(0).ErrorMessage);
            }
            await _guitarOperations.Add(request);
            return Unit.Value;
        }
    }
}
