// Pseudocode / Plan:
// 1. Define a command `CreateUserCommand` that carries a `UsersViewModel` payload.
// 2. Implement an `IRequestHandler<CreateUserCommand, UsersViewModel>` named `CreateUserHandler`.
// 3. Inject `IUsersOperations` into the handler via constructor and guard against null.
// 4. In `Handle`:
//    - Validate `request` and `request.User` are not null.
//    - Optionally validate required fields on the `UsersViewModel` (username/password).
//    - Call an async create method on `_userOperations` (assumed `CreateUserAsync`) to persist the user.
//    - Return the resulting `UsersViewModel` from the operation.
// 5. Propagate exceptions for upstream handling; do not swallow exceptions.
//
// Note: Adjust the called operation name if your `IUsersOperations` uses a different method name 
// (e.g., `AddUserAsync`, `CreateAsync`). This file focuses on the request handler implementation.

using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Application.EntityOperationsInterface;
using Application1.ViewModels;

namespace Application1.Features.Users.Commands.Handlers
{
    public class CreateUserCommand : IRequest<UsersViewModel>
    {
        public CreateUserCommand(UsersViewModel user) => User = user;
        public UsersViewModel User { get; set; }
    }

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