using BasicCRUD.Domain.Context;
using BasicCRUD.Domain.Models.Users.Commands;
using EnsureThat;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasicCRUD.Application.CommandHandlers.Users
{
    internal sealed class DeleteUserHandler : AsyncRequestHandler<DeleteUser>
    {
        private readonly IApplicationContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DeleteUserHandler(
            IApplicationContext context,
            UserManager<IdentityUser> userManager)
        {
            EnsureArg.IsNotNull(context, nameof(context));
            EnsureArg.IsNotNull(userManager, nameof(userManager));

            _context = context;
            _userManager = userManager;
        }

        protected async override Task Handle(DeleteUser command, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(command, nameof(command));

            var user = await _context.Users.Where(a => a.LoginId == command.Id).FirstOrDefaultAsync();
            var aspUser = await _userManager.FindByIdAsync(command.Id.ToString());

            if (aspUser != null && user != null)
            {
                await _userManager.DeleteAsync(aspUser);

                _context.Users.Remove(user);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
