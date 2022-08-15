using BasicCRUD.Domain.Models.Users.Commands;
using EnsureThat;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace BasicCRUD.Application.CommandHandlers.Users
{
    internal sealed class UpdateUserPasswordHandler : IRequestHandler<UpdateUserPassword, string>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UpdateUserPasswordHandler(
            UserManager<IdentityUser> userManager)
        {
            EnsureArg.IsNotNull(userManager, nameof(userManager));

            _userManager = userManager;
        }

        public async Task<string> Handle(UpdateUserPassword command, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(command, nameof(command));
            EnsureArg.IsNotEmptyOrWhiteSpace(command.CurrentPassword, nameof(command.CurrentPassword));
            EnsureArg.IsNotEmptyOrWhiteSpace(command.NewPassword, nameof(command.NewPassword));

            var aspUser = await _userManager.FindByIdAsync(command.Id.ToString());

            if (aspUser != null)
            {
                await _userManager.ChangePasswordAsync(aspUser, command.CurrentPassword, command.NewPassword);

                return aspUser.Id;
            }

            return default;
        }
    }
}
