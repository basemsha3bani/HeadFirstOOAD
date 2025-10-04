using Application1.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application1.Features.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UsersViewModel>
    {
        public GetUserByIdQuery(UsersViewModel searchCriteria)
        {
            this.searchCriteria = searchCriteria;
        }

        public UsersViewModel searchCriteria { get; set; } = new UsersViewModel();
    }
}
