using Application.EntityOperationsInterface;
using Application1.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Handlers
{
    internal class CreateUserHandler : IRequestHandler<CreateUserCommand, UsersViewModel>
    {
        private readonly IUsersOperations _userOperations;

        public CreateUserHandler(IUsersOperations userOperations)
        {
            _userOperations = userOperations ?? throw new ArgumentNullException(nameof(userOperations));
        }

        public async Task<UsersViewModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.User == null) throw new ArgumentNullException(nameof(request.User));

            // Basic validation - ensure required fields are provided
            if (string.IsNullOrWhiteSpace(request.User.UserName))
            {
                return new UsersViewModel
                {
                    UserName = request.User.UserName,
                    IsAuthenticated = false,
                    Message = "UserName is required."
                };
            }

            if (string.IsNullOrWhiteSpace(request.User.Password))
            {
                return new UsersViewModel
                {
                    UserName = request.User.UserName,
                    IsAuthenticated = false,
                    Message = "Password is required."
                };
            }

            // Call into the operations layer to create the user.
            // Expected method name on IUsersOperations: CreateUserAsync.
            // If your interface uses a different method name, update accordingly.
            var result = await _userOperations.CreateUserAsync(request.User);

            return result;
        }
    }
}
