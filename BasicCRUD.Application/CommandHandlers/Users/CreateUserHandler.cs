using BasicCRUD.Domain.Context;
using BasicCRUD.Domain.Models.Users;
using BasicCRUD.Domain.Models.Users.Commands;
using EnsureThat;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasicCRUD.Application.CommandHandlers.Users
{
    internal sealed class CreateUserHandler : IRequestHandler<CreateUser, Guid>
    {
        private readonly IApplicationContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateUserHandler(
            IApplicationContext context,
            UserManager<IdentityUser> userManager)
        {
            EnsureArg.IsNotNull(context, nameof(context));
            EnsureArg.IsNotNull(userManager, nameof(userManager));

            _context = context;
            _userManager = userManager;
        }

        public async Task<Guid> Handle(CreateUser command, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(command, nameof(command));
            EnsureArg.IsNotEmptyOrWhiteSpace(command.Email, nameof(command.Email));
            EnsureArg.IsNotEmptyOrWhiteSpace(command.FirstName, nameof(command.FirstName));
            EnsureArg.IsNotEmptyOrWhiteSpace(command.LastName, nameof(command.LastName));

            var newId = Guid.NewGuid();

            var identityUser = new IdentityUser
            {
                UserName = $"{command.FirstName}_{command.LastName}",
                Email = command.Email,
                Id = newId.ToString(),
            };

            await _userManager.CreateAsync(identityUser);
            await _userManager.AddPasswordAsync(identityUser, command.Password);

            var newUser = new User(newId,
                                   identityUser.Email,
                                   command.FirstName,
                                   command.LastName);

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync(cancellationToken);
            return newUser.LoginId;
        }
    }
}
