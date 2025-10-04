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
    internal class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UsersViewModel>
    {
        private readonly IUsersOperations _UserOperations;


        public GetUserByIdHandler(IUsersOperations UserOperations)
        {
            _UserOperations = UserOperations ?? throw new ArgumentNullException(nameof(UserOperations));
        }



        public async Task<UsersViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            //var User = await _UserOperations.CheckPasswordAsync(request.searchCriteria);
            return new UsersViewModel { UserName = request.searchCriteria.UserName,IsAuthenticated=true };
            UserLoginEvent @event = new UserLoginEvent { UserName = request.searchCriteria.UserName };

           // return (User??null);
        }
    }
}

   

