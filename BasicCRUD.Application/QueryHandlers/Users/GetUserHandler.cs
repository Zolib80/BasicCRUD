using BasicCRUD.Domain.Context;
using BasicCRUD.Domain.Models.Users;
using BasicCRUD.Domain.Models.Users.Queries;
using EnsureThat;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasicCRUD.Application.QueryHandlers.Users
{
    internal sealed class GetUserHandler : IRequestHandler<GetUser, User>
    {
        private readonly IApplicationContext _context;

        public GetUserHandler(
            IApplicationContext context)
        {
            EnsureArg.IsNotNull(context, nameof(context));

            _context = context;
        }

        public async Task<User> Handle(GetUser query, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(query, nameof(query));

            return await _context.Users.Where(a => a.LoginId == query.Id).FirstOrDefaultAsync();
        }
    }
}
