using BasicCRUD.Domain.Context;
using BasicCRUD.Domain.Models.Users.Commands;
using EnsureThat;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasicCRUD.Application.CommandHandlers.Users
{
    internal sealed class UpdateUserHandler : IRequestHandler<UpdateUser, Guid>
    {
        private readonly IApplicationContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UpdateUserHandler(
            IApplicationContext context,
            UserManager<IdentityUser> userManager)
        {
            EnsureArg.IsNotNull(context, nameof(context));
            EnsureArg.IsNotNull(userManager, nameof(userManager));

            _context = context;
            _userManager = userManager;
        }

        public async Task<Guid> Handle(UpdateUser command, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(command, nameof(command));
            EnsureArg.IsNotEmptyOrWhiteSpace(command.Email, nameof(command.Email));
            EnsureArg.IsNotEmptyOrWhiteSpace(command.FirstName, nameof(command.FirstName));
            EnsureArg.IsNotEmptyOrWhiteSpace(command.LastName, nameof(command.LastName));

            var user = await _context.Users.Where(a => a.LoginId == command.Id).FirstOrDefaultAsync();
            var aspUser = await _userManager.FindByIdAsync(command.Id.ToString());

            if (aspUser != null && user != null)
            {
                aspUser.Email = command.Email;
                aspUser.UserName = $"{command.FirstName}_{command.LastName}";
                await _userManager.UpdateNormalizedEmailAsync(aspUser);
                await _userManager.UpdateNormalizedUserNameAsync(aspUser);

                user.SetFirstName(command.FirstName);
                user.SetLastName(command.LastName);
                user.SetEmail(command.Email);

                await _context.SaveChangesAsync(cancellationToken);

                return user.LoginId;
            }

            return default;
        }
    }
}
