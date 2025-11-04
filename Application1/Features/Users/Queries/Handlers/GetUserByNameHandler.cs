using Application.EntityOperationsInterface;
using Application1.Contracts;
using Application1.Mapping.ModelMappingInterface;
using Application1.ViewModels;
using Domain.Entities.Schema.dbo;
using Domain.Entities.Schema.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Events;

namespace Application1.Features.Users.Queries.Handlers
{
    internal class GetUserByNameHandler : IRequestHandler<GetUserByNameQuery, UsersViewModel>
    {
        private readonly IUsersOperations _UserOperations;


        public GetUserByNameHandler(IUsersOperations UserOperations)
        {
            _UserOperations = UserOperations ?? throw new ArgumentNullException(nameof(UserOperations));
        }



        public async Task<UsersViewModel> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
        {
            var User = await _UserOperations.CheckPasswordAsync(request.searchCriteria);
            return User;
           
        }
    }
}

   

