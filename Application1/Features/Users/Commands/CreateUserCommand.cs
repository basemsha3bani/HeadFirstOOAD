using Application1.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands
{
    public class CreateUserCommand : IRequest<UsersViewModel>
    {
        public CreateUserCommand(UsersViewModel user) => User = user;
        public UsersViewModel User { get; set; }
    }
}
